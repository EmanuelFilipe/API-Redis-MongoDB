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
        private readonly ICacheService _cacheService;
        private readonly string keyForCache = "news_cache";

        public NewsService(IMapper mapper, IMongoRepository<News> newsRepository, ICacheService cacheService)
        {
            _mapper = mapper;
            _newsRepository = newsRepository;
            _cacheService = cacheService;
        }

        public Result<NewsViewModel> Get(int page, int qtd)
        {
            var keyCache = $"{keyForCache}/{page}/{qtd}";
            var news = _cacheService.Get<Result<NewsViewModel>>(keyCache);

            if (news is null)
            {
                news = _mapper.Map<Result<NewsViewModel>>(_newsRepository.Get(page, qtd));
                _cacheService.Set(keyCache, news);
            }

            return news;
        }

        [HttpGet("{id:length(24)}", Name = "GetNews")]
        public NewsViewModel Get(string id)
        {
            var keyCache = $"{keyForCache}/{id}";
            var news = _cacheService.Get<NewsViewModel>(keyCache);

            if (news is null)
            {
                news = _mapper.Map<NewsViewModel>(_newsRepository.Get(id));
                _cacheService.Set(keyCache, news);
            }

            return news;
        }

        public NewsViewModel GetBySlug(string slug)
        {
            var keyCache = $"{keyForCache}/{slug}";
            var newsSlug = _cacheService.Get<NewsViewModel>(keyCache);

            if (newsSlug is null)
            {
                newsSlug = _mapper.Map<NewsViewModel>(_newsRepository.GetBySlug(slug));
                _cacheService.Set(keyCache, newsSlug);
            }
            return newsSlug;
        }

        public NewsViewModel Create(NewsViewModel news)
        {
            var entity = new News(news.Hat, news.Title, news.Text, news.Author, news.Img, news.Status);
            _newsRepository.Create(entity);

            var keyCache = $"{keyForCache}/{entity.Slug}";
            _cacheService.Set(keyCache, entity);

            return Get(entity.Id);
        }

        public void Update(string id, NewsViewModel newsViewModel)
        {
            var keyCache = $"{keyForCache}/{id}";
            _newsRepository.Update(id, _mapper.Map<News>(newsViewModel));

            _cacheService.Remove(keyCache);
            _cacheService.Set(keyCache, newsViewModel);
        }

        public void Remove(string id)
        {
            var keyCache = $"{keyForCache}/{id}";
            _newsRepository.Remove(id);
            _cacheService.Remove(keyCache);
        }
    }
}
