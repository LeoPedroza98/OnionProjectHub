using OnionProjectHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnionProjectHub.Service.Exceptions;

namespace OnionProjectHub.Service.Validations
{
    public class PedidoValidation
    {

        public void ValidaListaPedidos(List<Pedido> pedidos) 
        {
            if(pedidos.Count == 0) throw new OnionSaServiceException("A lista de pedidos está vázia. Revise os dados inseridos ou entre em contato com a equipe da Onion S.A e tente novamente.");
        }

        public void ValidaObjetoPedido(Pedido pedido) 
        {
            if (pedido == null) throw new OnionSaServiceException("O objeto está nulo ou vazio. Revise os dados inseridos ou entre em contato com a equipe da Onion S.A e tente novamente.");
            if (pedido.NumeroDoPedido <= 0) throw new OnionSaServiceException("É necessário informar o numero do pedido. Revise os dados inseridos ou entre em contato com a equipe da Onion S.A e tente novamente.");
            if (pedido.Cep.ToString().Length < 8 || pedido.Cep.ToString().Length > 8) throw new OnionSaServiceException("É necessário informar um CEP válido. Revise os dados inseridos ou entre em contato com a equipe da Onion S.A e tente novamente.");
            if (pedido.DocumentoCliente.ToString().Length != 11 && pedido.DocumentoCliente.ToString().Length != 14) throw new OnionSaServiceException("É necessário informar um CPF ou CNPJ válido. Revise os dados inseridos ou entre em contato com a equipe da Onion S.A e tente novamente.");
        }

        public void ValidaObjetoDadosCep(DadosCep dadosCep) 
        {
            if (dadosCep == null) throw new OnionSaServiceException("Não foi possivel localizar os dados do CEP informado. Valide o CEP inserido ou entre em contato com a equipe de suporte  da Onion S.A e tente novamente.");
            if (String.IsNullOrEmpty(dadosCep.UF)) throw new OnionSaServiceException("Não foi possivel localizar a UF do CEP informado. Valide o CEP inserido ou entre em contato com a equipe da Onion S.A e tente novamente.");
        }

        public void ValidaListaDePedidos(List<Pedido> lista) 
        {

            if(lista.Count <= 0) throw new OnionSaServiceException("A lista está vazia e sem nenhum pedido. Revise os dados inseridos ou entre em contato com a equipe da Onion S.A e tente novamente.");
        }



    }
}
