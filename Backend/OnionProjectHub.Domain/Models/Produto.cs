using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnionProjectHub.Domain.Models
{
    [Table("Produtos")]
    public class Produto
    {
        public Produto(long id, string nome, int preco)
        {
            Id = id;
            Nome = nome;
            Preco = preco;
        }

        [Key]
        public long Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Nome { get; set; }
        [Required]    
        public int Preco { get; set; }
        
        public static Produto Celular => new Produto(1, "Celular", 1000);
        public static Produto Notebook => new Produto(2, "Notebook", 3000);
        public static Produto Televisão => new Produto(3, "Televisão", 5000);
    
        public static Produto[] ObterDados()
        {
            return new Produto[]
            {
                Celular,
                Notebook,
                Televisão
            };
        }
    }
}
