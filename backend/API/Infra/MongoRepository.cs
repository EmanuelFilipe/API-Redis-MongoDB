using API.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;

namespace API.Infra
{
    public class MongoRepository<T> : IMongoRepository<T> where T : BaseEntity
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(IDatabaseSettings settings, string? collectionName = null)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);


            var name = string.IsNullOrWhiteSpace(collectionName)
                ? typeof(T).Name.ToLowerInvariant()
                : collectionName;

            //_collection = database.GetCollection<T>(typeof(T).Name.ToLower()); // pegando o nome da collection
            _collection = database.GetCollection<T>(name);
        }

        //public Result<T> Get(int page, int qtd)
        //{
        //    return _collection.Find<T>(news => news.Deleted == false).ToList();
        //}

        public Result<T> Get(int page, int qtd)
        {
            var result = new Result<T>();
            result.Page = page;
            result.Qtd = qtd;
            var filter = Builders<T>.Filter.Eq(news => news.Deleted, false);

            result.Data = _collection.Find(filter)
                                     .SortByDescending(entity => entity.PublishDate)
                                     .Skip((page - 1) * qtd)
                                     .Limit(qtd)
                                     .ToList();

            result.Total = _collection.CountDocuments(filter);
            result.TotalPages = result.Total / qtd;

            //long resultCalc = (long)Math.Ceiling((double)result.Total / qtd);
            //result.TotalPages = resultCalc < 0 ? 0 : resultCalc;

            return result;
        }

        public T GetBySlug(string slug)
        {
            return _collection.Find<T>(news => news.Slug.Equals(slug) && news.Deleted == false)
                              .FirstOrDefault();
        }

        public T Get(string id)
        {
            return _collection.Find<T>(news => news.Id.Equals(id) && news.Deleted == false)
                              .FirstOrDefault();
        }

        public T Create(T news)
        {
            _collection.InsertOne(news);
            return news;
        }

        public void Update(string id, T news)
        {
            _collection.ReplaceOne(news => news.Id.Equals(id), news);
        }

        public void Remove(string id)
        {
            var news = Get(id);

            if (news is null) return;

            news.Deleted = true;

            //deleção lógica
            _collection.ReplaceOne(news => news.Id.Equals(id), news);

            // deleção física
            //_collection.DeleteOne(news => news.Id.Equals(id));
        }
    }
}
