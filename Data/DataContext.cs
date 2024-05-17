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
    }
}