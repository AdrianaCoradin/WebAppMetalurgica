using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace WebAppMetalurgica.Controllers
{
    [Route("api/produtos")] 
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly string _caminhoImagens = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image"); 

        [HttpGet]
        public IActionResult GetProdutos()
        {
            if (!Directory.Exists(_caminhoImagens))
                return NotFound("A pasta de imagens não foi encontrada.");

            var arquivos = Directory.GetFiles(_caminhoImagens, "*.*") 
                .Where(f => f.EndsWith(".jpg") || f.EndsWith(".jpeg") || f.EndsWith(".png")) 
                .Select(f => Path.GetFileName(f)) 
                .ToList();

            var produtos = arquivos.Select(arquivo =>
            {
                var partes = Path.GetFileNameWithoutExtension(arquivo).Split('-');
                if (partes.Length == 2 && decimal.TryParse(partes[1].Trim(), out decimal preco))
                {
                    var nome = partes[0].Trim(); 
                    var precoFormatado = preco.ToString("0.##"); 
                    return new
                    {
                        Nome = nome,
                        Imagem = $"/image/{arquivo}",
                        Preco = decimal.Parse(precoFormatado) 
                    };
                }
                return null;
            }).Where(p => p != null).ToList();

            return Ok(produtos); 
        }
    }
}
