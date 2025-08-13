using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoEstagios.Api.Models
{
    public class Universidade
    {
        [Key]
        public int UniversidadeID { get; set; }
        public string Nome { get; set; }
        public string? EmailContacto { get; set; }
        public string? Telefone { get; set; }
    }

    public class Estudante
    {
        [Key]
        public int EstudanteID { get; set; }
        public string Nome { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public string? Curso { get; set; }

        [ForeignKey("Universidade")]
        public int UniversidadeID { get; set; }
        public Universidade Universidade { get; set; }
    }

    public class PedidoEstagio
    {
        [Key]
        public int PedidoID { get; set; }

        [ForeignKey("Estudante")]
        public int EstudanteID { get; set; }
        public Estudante Estudante { get; set; }

        public string? AreaEstagio { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string Estado { get; set; } = "Pendente";
        public DateTime DataSubmissao { get; set; } = DateTime.UtcNow;
        public string? Observacoes { get; set; }
    }

    public class DocumentoPedido
    {
        [Key]
        public int DocumentoID { get; set; }

        [ForeignKey("PedidoEstagio")]
        public int PedidoID { get; set; }
        public PedidoEstagio PedidoEstagio { get; set; }

        public string? TipoDocumento { get; set; }
        public string? CaminhoFicheiro { get; set; }
        public DateTime DataUpload { get; set; } = DateTime.UtcNow;
    }

    public class Utilizador
    {
        [Key]
        public int UtilizadorID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string PalavraPasseHash { get; set; }
        public string Perfil { get; set; }
    }
}
