using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly ILogger<NewsController> _logger;
        private readonly UploadService _uploadService;

        public UploadController(ILogger<NewsController> logger, UploadService uploadService)
        {
            _logger = logger;
            _uploadService = uploadService;
        }

        [HttpPost]
        public IActionResult Post(IFormFile file)
        {
            try
            {
                // validação caso não tenha sido enviado nenhuma imagem
                if (file is null || file.Length == 0) return null;

                // salvando a imagem na pasta Imagens dentro do projeto
                //using (var stream = new FileStream(Path.Combine("Imagens", image.FileName), FileMode.Create))
                //{
                //    image.CopyTo(stream);
                //}

                string urlFile = _uploadService.UploadFile(file);

                //urlImagem = $"http://localhost:5055/imgs/{webpNameImage}"
                return Ok(new
                {
                    mensagem = "Arquivo salvo com sucesso!",
                    urlImagem = urlFile
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro no upload da imagem: " + ex.Message);
            }
        }
            
    }
}
