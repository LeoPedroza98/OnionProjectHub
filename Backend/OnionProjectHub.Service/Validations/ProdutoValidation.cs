using OnionProjectHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnionProjectHub.Service.Exceptions;

namespace OnionProjectHub.Service.Validations
{
    public class ProdutoValidation
    {
        public void ValidaObjetoProduto(Produto produto) 
        {
            if (produto == null) throw new OnionSaServiceException("O objeto está nulo ou vazio. Revise os dados inseridos ou entre em contato com o suporte da Onion S.A e tente novamente.");
            if (String.IsNullOrEmpty(produto.Nome)) throw new OnionSaServiceException("O objeto não possui titulo. Revise os dados inseridos e tente novamente.");
            if (produto.Preco <= 1 ) throw new OnionSaServiceException("O objeto não possui um preço válido. Revise os dados inseridos e tente novamente.");
        }

        public void ValidaListaDeProdutos(List<Produto> lista) 
        {

            if(lista.Count <= 0) throw new OnionSaServiceException("A lista está vazia e sem nenhum produto. Revise os dados inseridos ou entre em contato com o suporte da Onion S.A e tente novamente.");
        }



    }
}
