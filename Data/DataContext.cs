using Microsoft.EntityFrameworkCore;
using OllamaProject.Entities;

namespace OllamaProject.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Preguntas> Preguntas { get; set; }
        public DbSet<Alternativas> Alternativas { get; set; }
        public DbSet<Formulario> Formulario { get; set; }
        public DbSet<Respuesta> Respuesta { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Prompt> Prompt { get; set; }

    }
}