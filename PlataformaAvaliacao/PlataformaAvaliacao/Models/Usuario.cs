using Microsoft.EntityFrameworkCore;

namespace PlataformaAvaliacao.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public string SenhaHash { get; set; }
        public string Papel { get; set; }
    }
}
