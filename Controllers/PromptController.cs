using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OllamaProject.Data;
using OllamaProject.Entities;

namespace OllamaProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromptController : ControllerBase
    {
        private readonly DataContext _dbContext;
        public PromptController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<IActionResult> Buscar()
        {
            var lista = await _dbContext.Prompt.ToListAsync();
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
            var prompt = await _dbContext.Prompt.FindAsync(id);
            return Ok(prompt);
        }

        [HttpPost]
        [Route("Agregar")]
        public async Task<IActionResult> Agregar([FromBody] Prompt prompt)
        {
            if (prompt == null)
            {
                return BadRequest("No se completaron los datos de respuesta");
            }
            await _dbContext.Prompt.AddAsync(prompt);
            await _dbContext.SaveChangesAsync();
            return Ok(prompt);
        }

        [HttpPut]
        [Route("Editar/id:int")]
        public async Task<IActionResult> Editar(int id, [FromBody] Prompt prompt)
        {
            var res = _dbContext.Prompt.Find(id);
            if (res == null)
            {
                return BadRequest("No existe el prompt buscado");
            }
            else
            {
                res.Id = prompt.Id;
                res.UserPrompt = prompt.UserPrompt;
                res.UsuarioID = prompt.UsuarioID;
                await _dbContext.SaveChangesAsync();
                return Ok(res);
            }
        }

        [HttpDelete]
        [Route("Eliminar/id:int")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var res = await _dbContext.Prompt.FindAsync(id);
            if (res == null)
            {
                return BadRequest("No existe el prompt");
            }
            else
            {
                _dbContext.Prompt.Remove(res);
                await _dbContext.SaveChangesAsync();
                return Ok(res);
            }
        }
    }
}
