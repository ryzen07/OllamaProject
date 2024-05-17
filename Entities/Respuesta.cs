namespace OllamaProject.Entities
{
    public class Respuesta
    {
        public int Id { get; set; }
        public int PreguntaID { get; set; }
        public int UsuarioID    { get; set; }
        public int AlternativaId { get;set; }
    }
}
