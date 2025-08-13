using Microsoft.AspNetCore.Mvc;
using GestaoEstagios.Api.Data;
using GestaoEstagios.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoEstagios.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentosController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public DocumentosController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadDocumento([FromForm] int pedidoId, [FromForm] string tipoDocumento, [FromForm] IFormFile ficheiro)
        {
            var pedido = await _db.PedidosEstagio.FindAsync(pedidoId);
            if (pedido == null)
                return NotFound(new { message = "Pedido não encontrado" });

            if (ficheiro == null || ficheiro.Length == 0)
                return BadRequest(new { message = "Ficheiro inválido" });

            var pastaUploads = Path.Combine(_env.ContentRootPath, "Uploads");
            if (!Directory.Exists(pastaUploads))
                Directory.CreateDirectory(pastaUploads);

            var nomeFicheiro = $"{Guid.NewGuid()}_{ficheiro.FileName}";
            var caminhoCompleto = Path.Combine(pastaUploads, nomeFicheiro);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await ficheiro.CopyToAsync(stream);
            }

            var doc = new DocumentoPedido
            {
                PedidoID = pedidoId,
                TipoDocumento = tipoDocumento,
                CaminhoFicheiro = nomeFicheiro
            };

            _db.DocumentosPedido.Add(doc);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Ficheiro carregado com sucesso", documentoId = doc.DocumentoID });
        }

        [HttpGet("pedido/{pedidoId}")]
        public async Task<IActionResult> ListarPorPedido(int pedidoId)
        {
            var docs = await _db.DocumentosPedido
                .Where(d => d.PedidoID == pedidoId)
                .OrderByDescending(d => d.DataUpload)
                .ToListAsync();

            return Ok(docs);
        }

        [HttpGet("download/{documentoId}")]
        public async Task<IActionResult> Download(int documentoId)
        {
            var doc = await _db.DocumentosPedido.FindAsync(documentoId);
            if (doc == null)
                return NotFound();

            var pastaUploads = Path.Combine(_env.ContentRootPath, "Uploads");
            var caminhoCompleto = Path.Combine(pastaUploads, doc.CaminhoFicheiro ?? "");
            if (!System.IO.File.Exists(caminhoCompleto))
                return NotFound(new { message = "Ficheiro não encontrado no servidor" });

            var mimeType = "application/octet-stream";
            return PhysicalFile(caminhoCompleto, mimeType, Path.GetFileName(caminhoCompleto));
        }
    }
}
