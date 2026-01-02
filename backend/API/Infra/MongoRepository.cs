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

        public List<T> Get()
        {
            return _collection.Find<T>(news => news.Deleted == false).ToList();
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
