using API.Entities;
using API.Entities.ViewModels;
using API.Infra;
using AutoMapper;

namespace API.Services
{
    public class GalleryService
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<Gallery> _galleryRepository;
        private readonly ICacheService _cacheService;
        private readonly string keyForCache = "gallery_cache";

        public GalleryService(IMapper mapper, IMongoRepository<Gallery> galleryRepository, ICacheService cacheService)
        {
            _mapper = mapper;
            _galleryRepository = galleryRepository;
            _cacheService = cacheService;
            //this.keyForCache = keyForCache;
        }

        public Result<GalleryViewModel> Get(int page, int qtd)
        {
            var keyCache = $"{keyForCache}/{page}/{qtd}";
            var gallery = _cacheService.Get<Result<GalleryViewModel>>(keyCache);

            if (gallery is null)
            {
                gallery = _mapper.Map<Result<GalleryViewModel>>(_galleryRepository.Get(page, qtd));
                _cacheService.Set(keyCache, gallery);
            }
            return gallery;
        }

        public GalleryViewModel Get(string id)
        {
            var keyCache = $"{keyForCache}/{id}";
            var gallery = _cacheService.Get<GalleryViewModel>(keyCache);

            if (gallery is null)
            {
                gallery = _mapper.Map<GalleryViewModel>(_galleryRepository.Get(id));
                _cacheService.Set(keyCache, gallery);
            }

            return gallery;
        }
        public GalleryViewModel GetBySlug(string slug)
        {
            var keyCache = $"{keyForCache}/{slug}";
            var gallerySlug = _cacheService.Get<GalleryViewModel>(keyCache);

            if (gallerySlug is null)
            {
                gallerySlug = _mapper.Map<GalleryViewModel>(_galleryRepository.GetBySlug(slug));
                _cacheService.Set(keyCache, gallerySlug);
            }
            return gallerySlug;
        }

        public GalleryViewModel Create(GalleryViewModel galleryViewModel)
        {
            var entity = new Gallery(
                galleryViewModel.Title,
                galleryViewModel.Legend,
                galleryViewModel.Author,
                galleryViewModel.Tags,
                galleryViewModel.Status,
                galleryViewModel.GalleryImages,
                galleryViewModel.Thumb
            );

            _galleryRepository.Create(entity);

            var keyCache = $"{keyForCache}/{entity.Slug}";
            _cacheService.Set(keyCache, entity);

            return Get(entity.Id);
        }

        public void Update(string id, GalleryViewModel galleryViewModel)
        {
            var keyCache = $"{keyForCache}/{id}";
            _galleryRepository.Update(id, _mapper.Map<Gallery>(galleryViewModel));

            _cacheService.Remove(keyCache);
            _cacheService.Set(keyCache, galleryViewModel);
        }   

        public void Remove(string id)
        {
            var keyCache = $"{keyForCache}/{id}";
            _galleryRepository.Remove(id);
            _cacheService.Remove(keyCache);
        }
    }
}
