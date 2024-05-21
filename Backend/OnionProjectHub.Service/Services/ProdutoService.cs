using OnionProjectHub.Domain.Models;
using OnionProjectHub.Repository.Context;
using OnionProjectHub.Repository.Interfaces;
using OnionProjectHub.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnionProjectHub.Service.Exceptions;
using OnionProjectHub.Service.Validations;

namespace OnionProjectHub.Service.Services
{
    public class ProdutoService
    {
        private readonly IProdutoRepository _repo;
        private readonly ProdutoValidation ProdutoValidation;
        public ProdutoService(IProdutoRepository repo)
        {
            _repo = repo;
            ProdutoValidation = new ProdutoValidation();
        }
        public void AdicionaProduto(Produto produto)
        {
            try
            {
                ProdutoValidation.ValidaObjetoProduto(produto);
                _repo.InserirProduto(produto);
            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar inserir o produto. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }
        public void AlteraProduto(Produto produto)
        {
            try
            {
                ProdutoValidation.ValidaObjetoProduto(produto);
                _repo.AlterarProduto(produto);

            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar alterar o produto. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }
        public async void RemoveProdutoPorId(int id)
        {
            try
            {
                var produto = await _repo.ObterProdutoPorId(id);

                ProdutoValidation.ValidaObjetoProduto(produto);

                _repo.RemoverProduto(produto);

            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar remover o cliente. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }
        public async Task<Produto> ObtemProdutoPorId(long id)
        {
            try
            {
                var produto = await _repo.ObterProdutoPorId(id);

                ProdutoValidation.ValidaObjetoProduto(produto);

                return produto;

            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar obter o produto. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }
        public async Task<Produto> ObtemProdutoPorTitulo(string titulo)
        {
            try
            {
                var produto = await _repo.ObterProdutoPorTitulo(titulo);

                ProdutoValidation.ValidaObjetoProduto(produto);

                return produto;

            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar obter o produto. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }
        public async Task<List<Produto>> ObtemTodosOsProdutos()
        {
            try
            {
                var produtos = await _repo.ObterTodosOsProdutos();

                ProdutoValidation.ValidaListaDeProdutos(produtos);

                return produtos;

            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar obter todos os Produtos. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }
    }
}
