using API.Entities;
using API.Entities.ViewModels;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GalleryController : ControllerBase
    {
        private readonly ILogger<GalleryController> _logger;
        private readonly GalleryService _galleryService;

        public GalleryController(ILogger<GalleryController> logger, GalleryService galleryService)
        {
            _logger = logger;
            _galleryService = galleryService;
        }

        [HttpGet("{page}/{qtd}")]
        public ActionResult<Result<GalleryViewModel>> Get(int page, int qtd) => _galleryService.Get(page, qtd);

        [HttpGet("{id:length(24)}", Name = "GetGallery")]
        public ActionResult<GalleryViewModel> Get(string id)
        {
            var news = _galleryService.Get(id);
            if (news is null) return NotFound();

            return news;
        }

        [HttpPost]
        public ActionResult<GalleryViewModel> Create(GalleryViewModel gallery)
        {
            var result = _galleryService.Create(gallery);

            return CreatedAtRoute("GetGallery", new { id = result.Id.ToString() }, result);
        }

        [HttpPut("{id:length(24)}")]
        public ActionResult<GalleryViewModel> Update(string id, GalleryViewModel galleryViewModel)
        {
            var news = _galleryService.Get(id);

            if (news is null) return NotFound();

            _galleryService.Update(id, galleryViewModel);

            return CreatedAtRoute("GetGallery", new { id }, galleryViewModel);
        }

        [HttpDelete("{id:length(24)}")]
        public ActionResult Delete(string id) 
        {
            var news = _galleryService.Get(id);

            if (news is null) return NotFound();

            _galleryService.Remove(id);

            return Ok(new { message = "Galeria deletada com sucesso!" });
        }
    }
}
