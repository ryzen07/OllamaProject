using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OllamaProject.Data;
using OllamaProject.Entities;

namespace OllamaProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class UsuarioController : ControllerBase
    {
        private readonly DataContext _dbContext;
        public UsuarioController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<IActionResult> Buscar()
        {
            var lista = await _dbContext.Usuario.ToListAsync();
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
            var usuario = await _dbContext.Usuario.FindAsync(id);
            return Ok(usuario);
        }

        [HttpPost]
        [Route("Agregar")]
        public async Task<IActionResult> Agregar([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest("No se completaron los datos del usuario");
            }
            await _dbContext.Usuario.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();
            return Ok(usuario);
        }

        [HttpPut]
        [Route("Editar/id:int")]
        public async Task<IActionResult> Editar(int id, [FromBody] Usuario usuario)
        {
            var res = _dbContext.Usuario.Find(id);
            if (res == null)
            {
                return BadRequest("No existe el usuario");
            }
            else
            {
                res.Id = usuario.Id;
                res.NombreApellido = usuario.NombreApellido;
                res.DNI = usuario.DNI;
                res.Correo = usuario.Correo;
                res.Telefono = usuario.Telefono;
                await _dbContext.SaveChangesAsync();
                return Ok(res);
            }
        }

        [HttpDelete]
        [Route("Eliminar/id:int")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var res = await _dbContext.Usuario.FindAsync(id);
            if (res == null)
            {
                return BadRequest("No existe el usuario");
            }
            else
            {
                _dbContext.Usuario.Remove(res);
                await _dbContext.SaveChangesAsync();
                return Ok(res);
            }
        }
    }
}