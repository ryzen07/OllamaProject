using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OllamaProject.Data;
using OllamaProject.Entities;

namespace OllamaProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormularioController : Controller
    {
        private readonly DataContext _dbContext;
        public FormularioController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<IActionResult> Buscar()
        {
             var lista = await _dbContext.Formulario.ToListAsync();
             return Ok(lista);
        }

        [HttpGet]
        [Route("Buscar/id:int")]
        public async Task<IActionResult> Buscar(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var formulario = await _dbContext.Formulario.FindAsync(id);
            return Ok(formulario);
        }

        [HttpPost]
        [Route("Agregar")]
        public async Task<IActionResult> Agregar([FromBody] Formulario formulario)
        {
            if (formulario == null)
            {
                return BadRequest("No se completó el formulario");
            }
            await _dbContext.Formulario.AddAsync(formulario);
            await _dbContext.SaveChangesAsync();
            return Ok(formulario);
        }

        [HttpPut]
        [Route("Editar/id:int")]
        public async Task<IActionResult> Editar(int id, [FromBody] Formulario formulario)
        {
            var res = _dbContext.Formulario.Find(id);
            if (res == null)
            {
                return BadRequest("No existe el formulario");
            }
            else
            {
                res.Id = formulario.Id;
                res.NombreForm = formulario.NombreForm;
                res.Descripcion = formulario.Descripcion;
                res.Area = formulario.Area;
                res.UsuarioId = formulario.UsuarioId;
                res.Telefono = formulario.Telefono;
                await _dbContext.SaveChangesAsync();
                return Ok(res);
            }
        }

        [HttpDelete]
        [Route("Eliminar/id:int")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var res = await _dbContext.Formulario.FindAsync(id);
            if (res == null)
            {
                return BadRequest("No existe el formulario");
            }
            else
            {
                _dbContext.Formulario.Remove(res);
                await _dbContext.SaveChangesAsync();
                return Ok(res);
            }
        }
    }
}
