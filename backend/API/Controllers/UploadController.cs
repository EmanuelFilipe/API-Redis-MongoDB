using API.Services;
using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly ILogger<NewsController> _logger;

        public UploadController(ILogger<NewsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post(IFormFile image)
        {
            try
            {
                // validação caso não tenha sido enviado nenhuma imagem
                if (image is null || image.Length == 0) return null;

                // salvando a imagem na pasta Imagens dentro do projeto
                using (var stream = new FileStream(Path.Combine("Imagens", image.FileName), FileMode.Create))
                {
                    image.CopyTo(stream);
                }

                // compreensao de imagem
                //var webpNameImage = Guid.NewGuid().ToString() + ".webp";

                //using (var webPFileStream = new FileStream(Path.Combine("Imagens", webpNameImage), FileMode.Create))
                //{
                //    using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
                //    {
                //        imageFactory?.Load(image.OpenReadStream())
                //            .Format(new WebPFormat())
                //            .Quality(100)
                //            .Save(webPFileStream);
                //    }
                //}
                //urlImagem = $"http://localhost:5055/imgs/{webpNameImage}"
                return Ok(new
                {
                    mensagem = "Imagem salva com sucesso!",
                    urlImagem = $"http://localhost:5055/imgs/{image.FileName}"
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro no upload da imagem: " + ex.Message);
            }
        }
            
    }
}
