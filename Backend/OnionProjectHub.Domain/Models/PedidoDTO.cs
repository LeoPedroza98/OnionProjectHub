using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionProjectHub.Domain.Models
{
    public class PedidoDTO
    {
        public string Documento { get; set; }
        public string RazaoSocial { get; set; }
        public string CEP { get; set; }
        public string Regiao { get; set; }
        public int Valor  { get; set; }
        public double ValorFinal { get; set; }
        public string Produto { get; set; }
        public int NumeroDoPedido { get; set; }
        public DateTime Data { get; set; }
        public DateTime DataEntrega { get; set; }
    }
}
