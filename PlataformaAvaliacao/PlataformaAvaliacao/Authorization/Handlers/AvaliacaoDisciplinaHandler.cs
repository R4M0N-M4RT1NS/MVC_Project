using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PlataformaAvaliacao.Authorization.Requirements;
using PlataformaAvaliacao.Data;
using System.Security.Claims;

namespace PlataformaAvaliacao.Authorization.Handlers
{
    public class AvaliacaoDisciplinaHandler : AuthorizationHandler<AvaliacaoDisciplinaRequirement>
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AvaliacaoDisciplinaHandler(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AvaliacaoDisciplinaRequirement requirement)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var routeData = httpContext?.GetRouteData();

            if (routeData == null)
            {
                context.Fail();
                return;
            }

            int disciplinaOfertadaId = 0;

            // Primeiro tenta pegar da rota
            if (routeData?.Values["disciplinaOfertadaId"] != null)
            {
                int.TryParse(routeData.Values["disciplinaOfertadaId"].ToString(), out disciplinaOfertadaId);
            }

            // Se não veio pela rota, tenta pegar do form (para POST)
            if (disciplinaOfertadaId == 0 && httpContext?.Request.HasFormContentType == true)
            {
                var form = await httpContext.Request.ReadFormAsync();
                if (form.TryGetValue("DisciplinaOfertadaId", out var formValue))
                {
                    int.TryParse(formValue.ToString(), out disciplinaOfertadaId);
                }
            }

            if (disciplinaOfertadaId == 0)
            {
                context.Fail();
                return;
            }

            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                context.Fail();
                return;
            }

            var usuarioId = int.Parse(userIdClaim.Value);

            var matriculado = await _context.Matriculas
                .AnyAsync(m => m.UsuarioId == usuarioId && m.DisciplinaOfertadaId == disciplinaOfertadaId);

            if (matriculado)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
