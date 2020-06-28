using Microsoft.AspNetCore.Mvc;
using WebMotorsRestAPI.Model;
using WebMotorsRestAPI.Business;

namespace WebMotorsRestAPI.Controllers
{
    //[Route("api/[controller]")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class WebMotorsAnuncioController : Controller
    {
        private readonly IAnuncioBusiness _anuncioBusiness;
        private readonly IMarcaBusiness _marcaBusiness;
        private readonly IModeloBusiness _modeloBusiness;
        private readonly IVersaoBusiness _versaoBusiness;
        private readonly IVeiculoBusiness _veiculoBusiness;

        public WebMotorsAnuncioController(IAnuncioBusiness anuncioBusiness, 
                                          IMarcaBusiness marcaBusiness, 
                                          IModeloBusiness modeloBusiness, 
                                          IVersaoBusiness versaoBusiness, 
                                          IVeiculoBusiness veiculoBusiness)
        {
            _anuncioBusiness = anuncioBusiness;
            _marcaBusiness = marcaBusiness;
            _modeloBusiness = modeloBusiness;
            _versaoBusiness = versaoBusiness;
            _veiculoBusiness = veiculoBusiness;
        }

        [HttpGet("PesquisarTodosAnuncios")]
        public IActionResult PesquisarTodosAnuncios()
        {
            var anuncios = _anuncioBusiness.FindAll();
            if (anuncios == null || anuncios.Count == 0)
            {
                return NotFound("Anuncios não Encontrados");
            }
            return Ok(_anuncioBusiness.FindAll());
        }

        [HttpGet("PesquisarVeiculos")]
        public IActionResult PesquisarVeiculos([FromBody] Filtro filtro)
        {
            if (filtro == null)
            {
                return BadRequest();
            }

            var veiculos = _veiculoBusiness.RetornaVeiculos(filtro.Marca, 
                                                           filtro.Modelo,
                                                           filtro.Versao,
                                                           filtro.Kilometragem,
                                                           filtro.Preco,
                                                           filtro.AnoModelo,
                                                           filtro.AnoFabricacao,
                                                           filtro.Cor);

            if (veiculos == null || veiculos.Count == 0)
            {
                return NotFound("Veiculos não Encontrados");
            }

            return new ObjectResult(veiculos);
        }


        [HttpGet("PesquisarAnuncioPorID/{id}")]
        public IActionResult PesquisarAnuncioPorID(int id)
        {
            var anuncio = _anuncioBusiness.FindByID(id);
            if (anuncio == null)
            {
                return NotFound("Anuncio não Encontrado");
            }
            return Ok(anuncio);
        }

        [HttpPost("CadastrarAnuncio")]
        public IActionResult CadastrarAnuncio([FromBody] Anuncio anuncio)
        {
            if (anuncio == null)
            {
                return BadRequest();
            }

            // Verifica se Esixte Marcas Cadastras
            var marcas = _marcaBusiness.PesquisaTodasMarcas();
            if (marcas == null || marcas.Count == 0)
            {
                return NotFound("Marcas não Encontradas");
            }

            //Verica se a Marca Especifica Existe
            Marca marca = _marcaBusiness.RetornaMarca(anuncio.Marca.ToString());
            if (marca == null)
            {
                return NotFound($"Marca {anuncio.Marca} não Encontrada!");
            }

            //Pegando o ID da Marca Selecionada e Verifica se Existe Modelos Cadastrados
            var modelos = _modeloBusiness.PesquisaTodosModelos(marca.ID);
            if (modelos == null || modelos.Count == 0)
            {
                return NotFound("Modelos não Encontradas");
            }

            // Retorna o Modelo Selecionado
            Modelo modelo = _modeloBusiness.RetornaModelo(marca.ID, anuncio.Modelo);
            if (modelo == null)
            {
                return NotFound($"Modelo {anuncio.Modelo} não Encontrado!");
            }

            //Pesquisa Versões de Veiculos 
            var versoes = _versaoBusiness.PesquisaTodoasVersoes(modelo.ID);
            if (versoes == null || versoes.Count == 0)
            {
                return NotFound("Versões não Encontradas");
            }

            Versao versao = _versaoBusiness.RetornaVersao(modelo.ID, anuncio.Versao);
            if (versao == null)
            {
                return NotFound($"Versão {anuncio.Versao} não Encontrada!");
            }

            return new ObjectResult(_anuncioBusiness.Create(anuncio));
        }

        [HttpPut("AlterarAnuncio")]
        public IActionResult AlterarAnuncio([FromBody] Anuncio anuncio)
        {
            if (anuncio == null)
            {
                return BadRequest();
            }
            return new ObjectResult(_anuncioBusiness.Update(anuncio));
        }

        [HttpDelete("DeletarAnuncio/{id}")]
        public IActionResult DeletarAnuncio(int id)
        {
            _anuncioBusiness.Delete(id);
            return NoContent();
        }
    }
}
