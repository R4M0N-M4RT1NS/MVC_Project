using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaAvaliacao.Data;
using PlataformaAvaliacao.Models;

namespace PlataformaAvaliacao.Controllers
{
    public class AvaliacaoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AvaliacaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Avaliacao
        public async Task<IActionResult> Index()
        {
            return View(await _context.Avaliacoes.ToListAsync());
        }

        // GET: Avaliacao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var avaliacao = await _context.Avaliacoes
                .FirstOrDefaultAsync(m => m.Id == id);

            if (avaliacao == null) return NotFound();

            return View(avaliacao);
        }

        // GET: Avaliacao/Create
        [Authorize(Policy = "PodeAvaliarDisciplina")]
        public async Task<IActionResult> Create(int disciplinaOfertadaId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            if (!int.TryParse(userIdClaim.Value, out int userId))
                return Unauthorized();

            // Buscar a matrícula do aluno na disciplina ofertada
            var matricula = await _context.Matriculas
                .FirstOrDefaultAsync(m => m.UsuarioId == userId && m.DisciplinaOfertadaId == disciplinaOfertadaId);

            if (matricula == null)
                return Forbid(); // aluno não está matriculado

            var avaliacao = new Avaliacao
            {
                MatriculaId = matricula.Id,
                DataAvaliacao = DateTime.Now
            };

            return View(avaliacao);
        }

        // POST: Avaliacao/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "PodeAvaliarDisciplina")]
        public async Task<IActionResult> Create([Bind("Nota,Comentario,Recomendarai")] Avaliacao avaliacao)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            if (!int.TryParse(userIdClaim.Value, out int userId))
                return Unauthorized();

            // Buscar a matrícula do aluno (última usada no formulário Create GET)
            var ultimaMatricula = await _context.Matriculas
                .Where(m => m.UsuarioId == userId)
                .OrderByDescending(m => m.Id)
                .FirstOrDefaultAsync();

            if (ultimaMatricula == null)
                return Forbid();

            if (ModelState.IsValid)
            {
                avaliacao.MatriculaId = ultimaMatricula.Id;
                avaliacao.DataAvaliacao = DateTime.Now;

                _context.Add(avaliacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(avaliacao);
        }

        // GET: Avaliacao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var avaliacao = await _context.Avaliacoes.FindAsync(id);
            if (avaliacao == null) return NotFound();

            return View(avaliacao);
        }

        // POST: Avaliacao/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MatriculaId,Nota,Comentario,Recomendarai,DataAvaliacao,DisciplinaOfertadaId")] Avaliacao avaliacao)
        {
            if (id != avaliacao.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(avaliacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvaliacaoExists(avaliacao.Id)) return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(avaliacao);
        }

        // GET: Avaliacao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var avaliacao = await _context.Avaliacoes
                .FirstOrDefaultAsync(m => m.Id == id);

            if (avaliacao == null) return NotFound();

            return View(avaliacao);
        }

        // POST: Avaliacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var avaliacao = await _context.Avaliacoes.FindAsync(id);
            if (avaliacao != null)
            {
                _context.Avaliacoes.Remove(avaliacao);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AvaliacaoExists(int id)
        {
            return _context.Avaliacoes.Any(e => e.Id == id);
        }
    }
}
