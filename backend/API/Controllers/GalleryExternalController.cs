using API.Entities;
using API.Entities.ViewModels;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GalleryExternalController : ControllerBase
    {
        private readonly ILogger<GalleryExternalController> _logger;
        private readonly GalleryService _galleryService;

        public GalleryExternalController(ILogger<GalleryExternalController> logger, GalleryService galleryService)
        {
            _logger = logger;
            _galleryService = galleryService;
        }

        [HttpGet("{page:int}/{qtd:int}")]
        public ActionResult<Result<GalleryViewModel>> Get(int page, int qtd) => _galleryService.Get(page, qtd);

        [HttpGet("{slug}")]
        public ActionResult<GalleryViewModel> GetBySlug(string slug)
        {
            var news = _galleryService.GetBySlug(slug);

            if (news is null) return NotFound();

            return news;
        }
    }
}
