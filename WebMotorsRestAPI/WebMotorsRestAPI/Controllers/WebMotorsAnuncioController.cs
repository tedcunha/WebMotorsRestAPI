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

        public WebMotorsAnuncioController(IAnuncioService anuncioService)
        {
            _anuncioService = anuncioService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_anuncioService.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var anuncio = _anuncioService.FindByID(id);
            if (anuncio == null)
            {
                return NotFound("Anuncio não Encontrado");
            }

            return Ok(anuncio);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Anuncio anuncio)
        {
            if (anuncio == null)
            {
                return BadRequest();
            }
            return new ObjectResult(_anuncioService.Create(anuncio));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Anuncio anuncio)
        {
            if (anuncio == null)
            {
                return BadRequest();
            }
            return new ObjectResult(_anuncioService.Update(anuncio));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _anuncioService.Delete(id);
            return NoContent();
        }
    }
}
