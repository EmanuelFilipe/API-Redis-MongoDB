using API.Entities;
using API.Entities.ViewModels;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly ILogger<NewsController> _logger;
        private readonly VideoService _videosService;

        public VideoController(ILogger<NewsController> logger, VideoService videosService)
        {
            _logger = logger;
            _videosService = videosService;
        }

        [HttpGet]
        public ActionResult<Result<VideoViewModel>> Get(int page, int qtd) => _videosService.Get(page, qtd);

        [HttpGet("{id:length(24)}", Name = "GetVideos")]
        public ActionResult<VideoViewModel> Get(string id)
        {
            var video = _videosService.Get(id);
            if (video is null) return NotFound();

            return video;
        }

        [HttpPost]
        public ActionResult<VideoViewModel> Create(VideoViewModel videos)
        {
            var result = _videosService.Create(videos);

            return CreatedAtRoute("GetVideos", new { id = result.Id.ToString() }, result);
        }

        [HttpPut("{id:length(24)}")]
        public ActionResult<VideoViewModel> Update(string id, VideoViewModel videosViewModel)
        {
            var videos = _videosService.Get(id);

            if (videos is null) return NotFound();

            _videosService.Update(id, videosViewModel);

            return CreatedAtRoute("GetNews", new { id }, videosViewModel);
        }

        [HttpDelete("{id:length(24)}")]
        public ActionResult Delete(string id) 
        {
            var video = _videosService.Get(id);

            if (video is null) return NotFound();

            _videosService.Remove(id);

            return Ok(new { message = "Vídeo deletado com sucesso!" });
        }
    }
}
