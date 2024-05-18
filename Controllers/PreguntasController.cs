using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OllamaProject.Data;
using OllamaProject.Entities;

namespace OllamaProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreguntasController : ControllerBase
    {
        private readonly DataContext _dbContext;
        public PreguntasController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<IActionResult> Buscar()
        {
            var lista = await _dbContext.Preguntas.ToListAsync();
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
            var preguntas = await _dbContext.Preguntas.FindAsync(id);
            return Ok(preguntas);
        }

        [HttpPost]
        [Route("Agregar")]
        public async Task<IActionResult> Agregar([FromBody] Preguntas preguntas)
        {
            if (preguntas == null)
            {
                return BadRequest("No se completaron los datos de la pregunta");
            }
            await _dbContext.Preguntas.AddAsync(preguntas);
            await _dbContext.SaveChangesAsync();
            return Ok(preguntas);
        }

        [HttpPut]
        [Route("Editar/id:int")]
        public async Task<IActionResult> Editar(int id, [FromBody] Preguntas preguntas)
        {
            var res = _dbContext.Preguntas.Find(id);
            if (res == null)
            {
                return BadRequest("No existe la pregunta buscada");
            }
            else
            {
                res.Id = preguntas.Id;
                res.FormularioId = preguntas.FormularioId;
                res.Consulta = preguntas.Consulta;
                await _dbContext.SaveChangesAsync();
                return Ok(res);
            }
        }

        [HttpDelete]
        [Route("Eliminar/id:int")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var res = await _dbContext.Preguntas.FindAsync(id);
            if (res == null)
            {
                return BadRequest("No existe la pregunta");
            }
            else
            {
                _dbContext.Preguntas.Remove(res);
                await _dbContext.SaveChangesAsync();
                return Ok(res);
            }
        }
    }
}
