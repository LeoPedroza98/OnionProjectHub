using OnionProjectHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnionProjectHub.Service.Exceptions;

namespace OnionProjectHub.Service.Validations
{
    public class ClienteValidation
    {
        public void ValidaListaClientes(List<Cliente> clientes) 
        {
            if(clientes.Count == 0) throw new OnionSaServiceException("A lista enviada está vázia. Revise os dados inseridos ou entre em contato com o suporte da Onion S.A e tente novamente.");
        }
        public void ValidaObjetoCliente(Cliente cliente) 
        {
            if (cliente == null) throw new OnionSaServiceException("O objeto está nulo ou vazio. Revise os dados inseridos ou entre em contato com o suporte da Onion S.A e tente novamente.");
            if (String.IsNullOrEmpty(cliente.RazaoSocial)) throw new OnionSaServiceException("É necessário informar a razão social do cliente. Revise os dados inseridos e tente novamente.");
            if (cliente.CPFCNPJ.ToString().Length != 11 && cliente.CPFCNPJ.ToString().Length != 14) throw new OnionSaServiceException("É necessário informar um CPF ou CNPJ válido. Revise os dados inseridos e tente novamente.");
        }

        public void ValidaListaDeClientes(List<Cliente> lista) 
        {

            if(lista.Count <= 0) throw new OnionSaServiceException("A lista está vazia e sem nenhum cliente. Revise os dados inseridos ou entre em contato com o suporte da Onion S.A e tente novamente.");
        }



    }
}
