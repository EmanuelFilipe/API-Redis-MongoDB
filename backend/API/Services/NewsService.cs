using API.Entities;
using API.Entities.ViewModels;
using API.Infra;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Services
{
    public class NewsService : IMongoRepository<NewsViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<News> _newsRepository;

        public NewsService(IMapper mapper, IMongoRepository<News> newsRepository)
        {
            _mapper = mapper;
            _newsRepository = newsRepository;
        }

        public Result<NewsViewModel> Get(int page, int qtd)
        {
            return _mapper.Map<Result<NewsViewModel>>(_newsRepository.Get(page, qtd));
        }

        [HttpGet("{id:length(24)}", Name = "GetNews")]
        public NewsViewModel Get(string id)
        {
            return _mapper.Map<NewsViewModel>(_newsRepository.Get(id));
        }

        public NewsViewModel GetBySlug(string slug)
        {
            return _mapper.Map<NewsViewModel>(_newsRepository.GetBySlug(slug));
        }

        public NewsViewModel Create(NewsViewModel news)
        {
            var entity = new News(news.Hat, news.Title, news.Text, news.Author, news.Img, news.Status);
            _newsRepository.Create(entity);

            return Get(entity.Id);
        }

        public void Update(string id, NewsViewModel news)
        {
            _newsRepository.Update(id, _mapper.Map<News>(news));
        }

        public void Remove(string id)
        {
            _newsRepository.Remove(id);
        }
    }
}
