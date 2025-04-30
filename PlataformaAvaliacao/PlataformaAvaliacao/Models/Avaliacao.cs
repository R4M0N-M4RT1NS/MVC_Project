namespace PlataformaAvaliacao.Models
{
    public class Avaliacao
    {
        public int Id { get; set; }
        public int MatriculaId { get; set; }
        public int Nota { get; set; }
        public string Comentario { get; set; }
        public bool Recomendarai { get; set; }
        public DateTime DataAvaliacao { get; set; }
    }
}
