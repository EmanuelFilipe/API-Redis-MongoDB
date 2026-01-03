using API.Entities;
using API.Entities.ViewModels;
using API.Infra;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Services
{
    public class VideoService //: IMongoRepository<NewsViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IMongoRepository<Video> _videoRepository;

        public VideoService(IMapper mapper, IMongoRepository<Video> videoRepository)
        {
            _mapper = mapper;
            _videoRepository = videoRepository;
        }

        public Result<VideoViewModel> Get(int page, int qtd)
        {
            return _mapper.Map<Result<VideoViewModel>>(_videoRepository.Get(page, qtd));
        }

        [HttpGet("{id:length(24)}", Name = "GetNews")]
        public VideoViewModel Get(string id)
        {
            return _mapper.Map<VideoViewModel>(_videoRepository.Get(id));
        }

        public VideoViewModel GetBySlug(string slug)
        {
            return _mapper.Map<VideoViewModel>(_videoRepository.GetBySlug(slug));
        }

        public VideoViewModel Create(VideoViewModel video)
        {
            var entity = new Video(video.Hat, video.Title, video.Author, video.Thumbnail, video.UrlVideo, video.Status);
            _videoRepository.Create(entity);

            return Get(entity.Id);
        }

        public void Update(string id, VideoViewModel video)
        {
            _videoRepository.Update(id, _mapper.Map<Video>(video));
        }

        public void Remove(string id)
        {
            _videoRepository.Remove(id);
        }
    }
}
