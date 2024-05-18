using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OllamaProject.Data;
using OllamaProject.Entities;

namespace OllamaProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RespuestaController : ControllerBase
    {
        private readonly DataContext _dbContext;
        public RespuestaController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<IActionResult> Buscar()
        {
            var lista = await _dbContext.Respuesta.ToListAsync();
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
            var respuesta = await _dbContext.Respuesta.FindAsync(id);
            return Ok(respuesta);
        }

        [HttpPost]
        [Route("Agregar")]
        public async Task<IActionResult> Agregar([FromBody] Respuesta respuesta)
        {
            if (respuesta == null)
            {
                return BadRequest("No se completaron los datos de respuesta");
            }
            await _dbContext.Respuesta.AddAsync(respuesta);
            await _dbContext.SaveChangesAsync();
            return Ok(respuesta);
        }

        [HttpPut]
        [Route("Editar/id:int")]
        public async Task<IActionResult> Editar(int id, [FromBody] Respuesta respuesta)
        {
            var res = _dbContext.Respuesta.Find(id);
            if (res == null)
            {
                return BadRequest("No existe la respuesta buscada");
            }
            else
            {
                res.Id = respuesta.Id;
                res.PreguntaID = respuesta.PreguntaID;
                res.UsuarioID = respuesta.UsuarioID;
                res.AlternativaId = respuesta.AlternativaId;
                await _dbContext.SaveChangesAsync();
                return Ok(res);
            }
        }

        [HttpDelete]
        [Route("Eliminar/id:int")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var res = await _dbContext.Respuesta.FindAsync(id);
            if (res == null)
            {
                return BadRequest("No existe la respuesta");
            }
            else
            {
                _dbContext.Respuesta.Remove(res);
                await _dbContext.SaveChangesAsync();
                return Ok(res);
            }
        }
    }
}
