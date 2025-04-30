namespace PlataformaAvaliacao.Models
{
    public class DisciplinaOfertada
    {
        public int Id { get; set; }
        public int DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; }
        public int ProfessorId { get; set; }
        public Professor Professor { get; set; }
        public int Ano { get; set; }
        public int Semestre { get; set; }
    }
}
