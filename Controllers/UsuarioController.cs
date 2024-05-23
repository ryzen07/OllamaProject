using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OllamaProject.Data;
using OllamaProject.DTO;
using OllamaProject.Entities;
using OllamaProject.Services;

namespace OllamaProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class UsuarioController : ControllerBase
    {
        private readonly DataContext _dbContext;
        private readonly IMessage _emailService;
        public UsuarioController(DataContext dbContext, IMessage emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
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
        [HttpPost]
        [Route("EnviarMail")]
        public IActionResult SendEmail(SendEmailRequest request)
        {
            _emailService.SendEmail(request.Subject, request.Body, request.to);
            return Ok();
        }
    }

}

