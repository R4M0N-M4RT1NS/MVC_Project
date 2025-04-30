using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlataformaAvaliacao.Models;

namespace PlataformaAvaliacao.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Disciplina> Disciplinas { get; set; }
    public DbSet<Professor> Professores { get; set; }
    public DbSet<DisciplinaOfertada> DisciplinasOfertadas { get; set; }
    public DbSet<Matricula> Matriculas { get; set; }
    public DbSet<Avaliacao> Avaliacoes { get; set; }
}
