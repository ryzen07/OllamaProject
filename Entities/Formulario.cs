namespace OllamaProject.Entities
{
    public class Formulario
    {
        public int Id { get; set; }
        public string NombreForm { get; set; }
        public string Descripcion { get; set; }
        public string Area { get; set; }
        public int UsuarioId { get; set; }
        public int Telefono { get; set; }
    }
}
