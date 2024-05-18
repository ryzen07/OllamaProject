using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OllamaProject.Data;
using OllamaProject.Entities;

namespace OllamaProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlternativasController : ControllerBase
    {
        private readonly DataContext _dbContext;
        public AlternativasController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<IActionResult> Buscar()
        {
            var lista = await _dbContext.Alternativas.ToListAsync();
            return Ok(lista);
        }

        [HttpGet]
        [Route("Buscar/id:int")]
        public async Task<IActionResult> Buscar(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var alternativa = await _dbContext.Alternativas.FindAsync(id);
            return Ok(alternativa);
        }

        [HttpPost]
        [Route("Agregar")]
        public async Task<IActionResult> Agregar([FromBody] Alternativas alternativa)
        {
            if (alternativa == null)
            {
                return BadRequest("No se completaron los datos de alternativa");
            }
            await _dbContext.Alternativas.AddAsync(alternativa);
            await _dbContext.SaveChangesAsync();
            return Ok(alternativa);
        }

        [HttpPut]
        [Route("Editar/id:int")]
        public async Task<IActionResult> Editar(int id, [FromBody] Alternativas alternativa)
        {
            var res = _dbContext.Alternativas.Find(id);
            if (res == null)
            {
                return BadRequest("No existe la alternativa buscada");
            }
            else
            {
                res.Id = alternativa.Id;
                res.PreguntaID = alternativa.PreguntaID;
                res.Alternativa = alternativa.Alternativa;
                await _dbContext.SaveChangesAsync();
                return Ok(res);
            }
        }

        [HttpDelete]
        [Route("Eliminar/id:int")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var res = await _dbContext.Alternativas.FindAsync(id);
            if (res == null)
            {
                return BadRequest("No existe la alternativa");
            }
            else
            {
                _dbContext.Alternativas.Remove(res);
                await _dbContext.SaveChangesAsync();
                return Ok(res);
            }
        }
    }
}
