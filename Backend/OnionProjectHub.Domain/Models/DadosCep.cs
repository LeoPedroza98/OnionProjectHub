using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionProjectHub.Domain.Models
{
    public class DadosCep
    {
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string? Complemento { get; set; } = string.Empty;
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
        public int IBGE { get; set; }
        public int? GIA { get; set; } = 0;
        public int DDD { get; set; }
        public int Siafi { get; set; }
    }

}
