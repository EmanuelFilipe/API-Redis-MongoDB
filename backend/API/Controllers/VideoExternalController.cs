using API.Entities;
using API.Entities.ViewModels;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoExternalController : ControllerBase
    {
        private readonly ILogger<VideoController> _logger;
        private readonly VideoService _videoService;

        public VideoExternalController(ILogger<VideoController> logger, VideoService videoService)
        {
            _logger = logger;
            _videoService = videoService;
        }

        [HttpGet]
        public ActionResult<Result<VideoViewModel>> Get(int page, int qtd) => _videoService.Get(page, qtd);

        [HttpGet("{slug}")]
        public ActionResult<VideoViewModel> GetBySlug(string slug)
        {
            var news = _videoService.GetBySlug(slug);

            if (news is null) return NotFound();

            return news;
        }
    }
}
