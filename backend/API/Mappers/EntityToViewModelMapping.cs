using API.Entities;
using API.Entities.ViewModels;
using AutoMapper;

namespace API.Mappers
{
    public class EntityToViewModelMapping : Profile
    {
        public EntityToViewModelMapping() 
        {
            #region [Entidades]

                CreateMap<News, NewsViewModel>();
                CreateMap<Video, VideoViewModel>();
                CreateMap<Gallery, GalleryViewModel>();

            #endregion

            #region [Result<T>]

                CreateMap<Result<News>, Result<NewsViewModel>>().ReverseMap();
                CreateMap<Result<Video>, Result<VideoViewModel>>().ReverseMap();
                CreateMap<Result<Gallery>, Result<GalleryViewModel>>().ReverseMap();

            #endregion
        }
    }
}
