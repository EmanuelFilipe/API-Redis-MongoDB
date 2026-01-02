using API.Entities.ViewModels;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsExternalTestController : ControllerBase
    {
        private readonly ILogger<NewsController> _logger;
        private readonly NewsService _newsService;

        public NewsExternalTestController(ILogger<NewsController> logger, NewsService newsService)
        {
            _logger = logger;
            _newsService = newsService;
        }

        //[HttpGet]
        //public ActionResult<List<NewsViewModel>> Get() => _newsService.Get();

        [HttpGet]
        public ActionResult<NewsViewModel> GetBySlug(string slug)
        {
            var news = _newsService.GetBySlug(slug);

            if (news is null) return NotFound();

            return news;
        }

        //[HttpPost]
        //public ActionResult<NewsViewModel> Create(NewsViewModel news)
        //{
        //    var result = _newsService.Create(news);

        //    return CreatedAtRoute("GetNews", new { id = result.Id.ToString() }, result);
        //}

        //[HttpPut("{id:length(24)}")]
        //public ActionResult<NewsViewModel> Update(string id, NewsViewModel newsViewModel)
        //{
        //    var news = _newsService.Get(id);

        //    if (news is null) return NotFound();

        //    _newsService.Update(id, newsViewModel);

        //    return CreatedAtRoute("GetNews", new { id }, newsViewModel);
        //}

        //[HttpDelete("{id:length(24)}")]
        //public ActionResult Delete(string id) 
        //{
        //    var news = _newsService.Get(id);

        //    if (news is null) return NotFound();

        //    _newsService.Remove(id);

        //    return Ok(new { message = "Notícia deletada com sucesso!" });
        //}
    }
}
