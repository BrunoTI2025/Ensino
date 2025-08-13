using System.ComponentModel.DataAnnotations;

namespace gestao_estagios.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public string Universidade { get; set; } = "";
        public string Curso { get; set; } = "";
        public string? NomeEstudante { get; set; }
        public string AreaEstagio { get; set; } = "";
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public EstadoPedido EstadoAtual { get; set; } = EstadoPedido.Pendente;
        public List<PedidoHistorico> Historico { get; set; } = new();
    }
}
