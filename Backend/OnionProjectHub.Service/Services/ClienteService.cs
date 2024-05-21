using OnionProjectHub.Domain.Models;
using OnionProjectHub.Repository.Context;
using OnionProjectHub.Repository.Interfaces;
using System.Data;
using OnionProjectHub.Service.Exceptions;
using OnionProjectHub.Service.Validations;

namespace OnionProjectHub.Service.Services
{
    public class ClienteService
    {
		private readonly IClienteRepository _repo;
        private readonly OnionSaContext _cntxt;
        private readonly ClienteValidation clienteValidation;
        public ClienteService(IClienteRepository repo)
        {
            _repo = repo;
            clienteValidation = new ClienteValidation();
        }
        
        public Cliente CriaObjetoCliente(DataRow linha)
        {
            try
            {
                Cliente cliente = new Cliente()
                {
                    CPFCNPJ = linha[0].ToString(),
                    RazaoSocial = linha[1].ToString()
                    
                };

                return cliente;
            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar inserir o pedido. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }
        public void AdicionaCliente(Cliente cliente)
        {
            try
            {
                clienteValidation.ValidaObjetoCliente(cliente);
                _repo.InserirCliente(cliente);
            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar inserir o cliente. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }
        
        public void AdicionaVariosClientes(List<Cliente> clientes)
        {
            try
            {
                clienteValidation.ValidaListaClientes(clientes);
                _repo.InserirVariosClientes(clientes);
            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar inserir a lista de clientes. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }
        
        public void AlteraCliente(Cliente cliente)
        {
            try
            {
                clienteValidation.ValidaObjetoCliente(cliente);
                _repo.AlterarCliente(cliente);

            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar alterar o cliente. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }
        public async void RemoveClientePorDoc(string documento)
        {
            try
            {
                var cliente = await _repo.ObterClientePorDoc(documento);

                clienteValidation.ValidaObjetoCliente(cliente);

                _repo.RemoverCliente(cliente);

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
        
        public async Task<Cliente> ObtemClientePorDoc(string documento)
        {
            try
            {
                var cliente = await _repo.ObterClientePorDoc(documento);

                clienteValidation.ValidaObjetoCliente(cliente);

                return cliente;

            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar obter o cliente. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }
        public async Task<List<Cliente>> ObtemTodosOsClientes() 
        {
            try
            {
                var clientes = await _repo.ObterTodosOsClientes();

                clienteValidation.ValidaListaDeClientes(clientes);

                return clientes;

            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar obter todos os clientes. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }
    }
}
