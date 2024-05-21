using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnionProjectHub.Domain.Models
{
    [Table("Pedidos")]
    public class Pedido
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public int NumeroDoPedido { get; set; }

        [Required]
        public string DocumentoCliente { get; set; }

        [ForeignKey("CPFCNPJ")]
        public Cliente Cliente { get; set; }

        [Required]
        public long ProdutoId { get; set; }
        [ForeignKey("ProdutoId")]
        public Produto Produto { get; set; }

        [Required]
        [MaxLength(8)]
        public string Cep { get; set; }

        [Required]
        public DateTime Data { get; set; }
    }
}
