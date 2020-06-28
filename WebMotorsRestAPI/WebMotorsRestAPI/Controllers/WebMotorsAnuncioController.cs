using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMotorsRestAPI.Model;
using WebMotorsRestAPI.Services;

namespace WebMotorsRestAPI.Controllers
{
    [Route("api/[controller]")]
    public class WebMotorsAnuncioController : Controller
    {
        private readonly IAnuncioService _anuncioService;
        private readonly IMarcaService _marcaService;
        private readonly IModeloService _modeloService;
        private readonly IVersaoService _versaoService;
        private readonly IVeiculoService _veiculoService;

        public WebMotorsAnuncioController(IAnuncioService anuncioService, 
                                          IMarcaService marcaService, 
                                          IModeloService modeloService, 
                                          IVersaoService versaoService, 
                                          IVeiculoService veiculoService)
        {
            _anuncioService = anuncioService;
            _marcaService = marcaService;
            _modeloService = modeloService;
            _versaoService = versaoService;
            _veiculoService = veiculoService;
        }

        [HttpGet("PesquisarTodosAnuncios")]
        public IActionResult PesquisarTodosAnuncios()
        {
            var anuncios = _anuncioService.FindAll();
            if (anuncios == null || anuncios.Count == 0)
            {
                return NotFound("Anuncios não Encontrados");
            }
            return Ok(_anuncioService.FindAll());
        }

        [HttpGet("PesquisarVeiculos")]
        public IActionResult PesquisarVeiculos([FromBody] Filtro filtro)
        {
            if (filtro == null)
            {
                return BadRequest();
            }

            if (filtro.Kilometragem == null)
            {
                filtro.Kilometragem = 0;
            }

            if (filtro.AnoModelo == null)
            {
                filtro.AnoModelo = 0;
            }

            if (filtro.AnoFabricacao == null)
            {
                filtro.AnoFabricacao = 0;
            }

            var veiculos = _veiculoService.RetornaVeiculos(filtro.Marca, 
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
            var anuncio = _anuncioService.FindByID(id);
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
            var marcas = _marcaService.PesquisaTodasMarcas();
            if (marcas == null || marcas.Count == 0)
            {
                return NotFound("Marcas não Encontradas");
            }

            //Verica se a Marca Especifica Existe
            Marca marca = _marcaService.RetornaMarca(anuncio.Marca.ToString());
            if (marca == null)
            {
                return NotFound($"Marca {anuncio.Marca} não Encontrada!");
            }

            //Pegando o ID da Marca Selecionada e Verifica se Existe Modelos Cadastrados
            var modelos = _modeloService.PesquisaTodosModelos(marca.ID);
            if (modelos == null || modelos.Count == 0)
            {
                return NotFound("Modelos não Encontradas");
            }

            // Retorna o Modelo Selecionado
            Modelo modelo = _modeloService.RetornaModelo(marca.ID, anuncio.Modelo);
            if (modelo == null)
            {
                return NotFound($"Modelo {anuncio.Modelo} não Encontrado!");
            }

            //Pesquisa Versões de Veiculos 
            var versoes = _versaoService.PesquisaTodoasVersoes(modelo.ID);
            if (versoes == null || versoes.Count == 0)
            {
                return NotFound("Versões não Encontradas");
            }

            Versao versao = _versaoService.RetornaVersao(modelo.ID, anuncio.Versao);
            if (versao == null)
            {
                return NotFound($"Versão {anuncio.Versao} não Encontrada!");
            }

            return new ObjectResult(_anuncioService.Create(anuncio));
        }

        [HttpPut("AlterarAnuncio")]
        public IActionResult AlterarAnuncio([FromBody] Anuncio anuncio)
        {
            if (anuncio == null)
            {
                return BadRequest();
            }
            return new ObjectResult(_anuncioService.Update(anuncio));
        }

        [HttpDelete("DeletarAnuncio/{id}")]
        public IActionResult DeletarAnuncio(int id)
        {
            _anuncioService.Delete(id);
            return NoContent();
        }
    }
}
