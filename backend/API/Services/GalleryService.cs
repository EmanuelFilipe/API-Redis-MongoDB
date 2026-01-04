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

        public GalleryService(IMapper mapper, IMongoRepository<Gallery> galleryRepository)
        {
            _mapper = mapper;
            _galleryRepository = galleryRepository;
        }

        public Result<GalleryViewModel> Get(int page, int qtd)
        {
            return _mapper.Map<Result<GalleryViewModel>>(_galleryRepository.Get(page, qtd));
        }

        public GalleryViewModel Get(string id)
        {
            return _mapper.Map<GalleryViewModel>(_galleryRepository.Get(id));
        }
        public GalleryViewModel GetBySlug(string slug)
        {
            return _mapper.Map<GalleryViewModel>(_galleryRepository.GetBySlug(slug));
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

            return Get(entity.Id);
        }

        public void Update(string id, GalleryViewModel galleryViewModel)
        {
            _galleryRepository.Update(id, _mapper.Map<Gallery>(galleryViewModel));
        }   

        public void Remove(string id)
        {
            _galleryRepository.Remove(id);
        }
    }
}
