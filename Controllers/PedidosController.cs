using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestaoEstagios.Api.Data;
using GestaoEstagios.Api.Models;

namespace GestaoEstagios.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public PedidosController(ApplicationDbContext db) { _db = db; }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? estado)
        {
            var q = _db.PedidosEstagio.Include(p => p.Estudante).ThenInclude(s => s.Universidade).AsQueryable();
            if (!string.IsNullOrEmpty(estado)) q = q.Where(p => p.Estado == estado);
            var list = await q.OrderByDescending(p => p.DataSubmissao).ToListAsync();
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePedidoDto dto)
        {
            var estudante = await _db.Estudantes.FindAsync(dto.EstudanteID);
            if (estudante == null) return BadRequest(new { message = "Estudante não encontrado" });

            var pedido = new PedidoEstagio
            {
                EstudanteID = dto.EstudanteID,
                AreaEstagio = dto.AreaEstagio,
                DataInicio = dto.DataInicio,
                DataFim = dto.DataFim,
                Observacoes = dto.Observacoes
            };

            _db.PedidosEstagio.Add(pedido);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = pedido.PedidoID }, pedido);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var p = await _db.PedidosEstagio.Include(x => x.Estudante).ThenInclude(s => s.Universidade).FirstOrDefaultAsync(x => x.PedidoID == id);
            if (p == null) return NotFound();
            return Ok(p);
        }

        [HttpPut("{id}/estado")]
        public async Task<IActionResult> UpdateEstado(int id, [FromBody] UpdateEstadoDto dto)
        {
            var p = await _db.PedidosEstagio.FindAsync(id);
            if (p == null) return NotFound();
            p.Estado = dto.Estado;
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }

    public record CreatePedidoDto(int EstudanteID, string AreaEstagio, DateTime? DataInicio, DateTime? DataFim, string? Observacoes);
    public record UpdateEstadoDto(string Estado);
}
