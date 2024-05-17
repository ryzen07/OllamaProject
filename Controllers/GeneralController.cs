using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OllamaProject.Entities;

namespace OllamaProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Preguntas>>> getPrompt()
        {
            var prompt = new List<Preguntas>
            {
                new Preguntas
                {
                    Id = 1,
                    FormularioId = 1,
                    Consulta = "¿De que color es el cielo?"
                }
            };
            return Ok(prompt);
        }

    }
}