using System.Security.Principal;

namespace PlataformaAvaliacao.Models
{
    public class Matricula
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int DisciplinaOfertadaId { get; set; }  // FK
        public DisciplinaOfertada DisciplinaOfertada { get; set; } 

    }
}
