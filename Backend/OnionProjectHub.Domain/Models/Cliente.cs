using System.ComponentModel.DataAnnotations;

namespace OnionProjectHub.Domain.Models
{
    public class Cliente
    {
        [Key]
        [MaxLength(14)]
        public string CPFCNPJ { get; set; }

        [Required]
        [MaxLength(250)]
        public string RazaoSocial { get; set; }


        public ICollection<Pedido> Pedidos { get; set; }
    }
}
