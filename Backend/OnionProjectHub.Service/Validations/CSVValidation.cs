using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnionProjectHub.Service.Exceptions;

namespace OnionProjectHub.Service.Validations
{
    public class CSVValidation
    {
        private List<String> produtos = new List<String>() { "Celular", "Notebook", "Televisão"};
        private static bool ValidaCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        private static bool ValidaCNPJ(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }

        public void ValidaLinhaDataTable(DataRow linha, int index) 
        {
            string documento = linha.ItemArray[0].ToString();
            string razaoSocial = linha.ItemArray[1].ToString();
            string CEP = linha.ItemArray[2].ToString();
            string produto = linha.ItemArray[3].ToString();
            string numeroPedido = linha.ItemArray[4].ToString();
            string data = linha.ItemArray[5].ToString();

            if (String.IsNullOrEmpty(documento)) throw new OnionSaServiceException($"O campo Documento da linha {index} não foi informado. Revise os dados inseridos ou entre em contato com a equipe da Onion S.A e tente novamente.");
            if (documento.Length > 14 || documento.Length < 11 || !(ValidaCPF(documento) || ValidaCNPJ(documento))) throw new OnionSaServiceException($"Não foi inserido um CPF/CNPJ válido no campo Documento da linha {linha}. Revise os dados inseridos ou entre em contato com a equipe da Onion S.A e tente novamente.");
            if (String.IsNullOrEmpty(CEP)) throw new OnionSaServiceException($"O campo CEP da linha {index} não foi informado. Revise os dados inseridos ou entre em contato com a equipe da Onion S.A e tente novamente.");
            if (CEP.Length != 8) throw new OnionSaServiceException($"Não foi inserido um CEP válido na linha {linha}. Revise os dados inseridos ou entre em contato com a equipe da Onion S.A e tente novamente.");
            if (String.IsNullOrEmpty(produto)) throw new OnionSaServiceException($"O campo Produto da linha {index} não foi informado. Revise os dados inseridos ou entre em contato com a equipe da Onion S.A e tente novamente.");
            if (!produtos.Any(p => p == produto)) throw new OnionSaServiceException($"O Produto da linha {index} não existe. Revise os dados inseridos ou entre em contato com a equipe da Onion S.A e tente novamente.");
            if(string.IsNullOrEmpty(numeroPedido) || Int32.Parse(numeroPedido) <= 0) throw new OnionSaServiceException($"O campo Número do Pedido da linha {index} não foi informado. Revise os dados inseridos ou entre em contato com a equipe da Onion S.A e tente novamente.");
            if(String.IsNullOrEmpty(data)) throw new OnionSaServiceException($"O campo Número do Data da linha {index} não foi informado. Revise os dados inseridos ou entre em contato com a equipe da Onion S.A e tente novamente.");
            if(!DateTime.TryParse(data, out DateTime dataS)) throw new OnionSaServiceException($"Não foi inserido uma Data válidaS na linha {linha}. Revise os dados inseridos ou entre em contato com a equipe da Onion S.A e tente novamente.");
        }
    }
}
