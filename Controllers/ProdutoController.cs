namespace WebAppMetalurgica.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.IO;
    using System.Linq;

    namespace WebAppMetalurgica.Controllers
    {
        [Route("api/produtos")]
        [ApiController]
        public class ProdutoController : ControllerBase
        {
            private readonly string _caminhoImagens = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image");

            [HttpGet]
            public IActionResult ObterProdutos()
            {
                if (!Directory.Exists(_caminhoImagens))
                    return NotFound("Ocorreu um erro, tente acessar mais tarde.");

                var arquivos = Directory.GetFiles(_caminhoImagens, "*.*")
                    .Where(f => f.EndsWith(".jpg") || f.EndsWith(".jpeg") || f.EndsWith(".png"))
                    .Select(f => Path.GetFileName(f))
                    .ToList();

                var produtos = arquivos.Select(arquivo =>
                {
                    var partes = Path.GetFileNameWithoutExtension(arquivo).Split('-');
                    return partes.Length == 2 && decimal.TryParse(partes[1], out decimal preco)
                        ? new
                        {
                            Nome = partes[0],
                            Imagem = $"/image/{arquivo}", 
                            Preco = preco
                        }
                        : null;
                }).Where(p => p != null).ToList();

                return Ok(produtos);
            }
        }
    }

}
