using Microsoft.AspNetCore.Mvc;
using PlataformaAvaliacao.Data;
using System;
using System.Linq;

namespace PlataformaAvaliacao.Controllers {
    public class DashController : Controller {
        private readonly ApplicationDbContext _context;

        public DashController(ApplicationDbContext context) {
            _context = context;
        }

        public IActionResult Index() {
            var avaliacoes = _context.Avaliacoes.ToList();

            // Média geral de todas as notas
            var mediaTotal = avaliacoes.Any() ? avaliacoes.Average(a => a.Nota) : 0;

            // Agrupa por data e calcula média de cada dia
            var mediasPorDia = avaliacoes
                .GroupBy(a => a.DataAvaliacao.Date)
                .Select(g => new {
                    Data = g.Key,
                    Media = g.Average(a => a.Nota)
                })
                .OrderBy(g => g.Data)
                .ToList();

            // Dados para o gráfico
            var labels = mediasPorDia.Select(m => m.Data.ToString("dd/MM/yyyy")).ToList();
            var dados = mediasPorDia.Select(m => Math.Round(m.Media, 2)).ToList();

            // Passando para a view
            ViewBag.MediaTotal = mediaTotal;
            ViewBag.MediasPorDia = mediasPorDia;
            ViewBag.Labels = labels;
            ViewBag.Dados = dados;

            return View();
        }
    }
}
