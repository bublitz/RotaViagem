using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RotaViagemAPI.Data;
using RotaViagemAPI.Models;

namespace RotaViagemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RotasController : ControllerBase
    {
        private readonly RotaContext _context;

        public RotasController(RotaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rota>>> GetRotas() =>
            await _context.Rotas.ToListAsync();

        [HttpPost]
        public async Task<ActionResult<Rota>> PostRota(Rota rota)
        {
            _context.Rotas.Add(rota);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRotas), new { origem = rota.Origem, destino = rota.Destino }, rota);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRota(string origem, string destino)
        {
            var rota = await _context.Rotas.FirstOrDefaultAsync(r => r.Origem == origem && r.Destino == destino);
            if (rota == null) return NotFound();

            _context.Rotas.Remove(rota);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
