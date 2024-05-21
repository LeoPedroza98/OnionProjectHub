using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using OnionProjectHub.Domain.Models;
using OnionProjectHub.Repository.Context;
using OnionProjectHub.Repository.Exceptions;
using OnionProjectHub.Repository.Interfaces;


namespace OnionProjectHub.Repository.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly OnionSaContext _cntxt;
        private readonly DbSet<Cliente> _dbSet;
        public ClienteRepository(OnionSaContext context)
        {
            _cntxt = context ?? throw new ArgumentNullException(nameof(context)); ;
            _dbSet = _cntxt.Set<Cliente>();
        }
        public void AlterarCliente<T>(T cliente) where T : Cliente
        {
            try
            {
                _dbSet.Update(cliente);
            }
            catch (CannotInsertNullException nullException)
            {
                throw new OnionSaRepositoryException($"Algum dos atributos obrigatórios do objeto foi enviado nulo ou vazio. Valide os dados inseridos e tente novamente.\nMais informações: {nullException.Message}");
            }
            catch(MaxLengthExceededException maxLenException)
            {
                throw new OnionSaRepositoryException($"Algum dos atributos do objeto ultrapassou a quantidade máxima de caracteres. Valide os dados inseridos e tente novamente.\nMais informações: {maxLenException.Message}");
            }
            catch(Exception ex)
            {
                throw new OnionSaRepositoryException($"Um erro ocorreu algo tentar atualizar o cliente. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }

        }
        
        public async void InserirCliente<T>(T cliente) where T : Cliente
        {
            try
            {
                await _dbSet.AddAsync(cliente);
            }
            catch (UniqueConstraintException nullException)
            {
                throw new OnionSaRepositoryException($"O número de cliente que você está tentando inserir já está em uso. Valide os dados inseridos e tente novamente.\nMais informações: {nullException.Message}");
            }
            catch (CannotInsertNullException nullException)
            {
                throw new OnionSaRepositoryException($"Algum dos atributos obrigatórios do objeto foi enviado nulo ou vazio. Valide os dados inseridos e tente novamente.\nMais informações: {nullException.Message}");
            }
            catch (MaxLengthExceededException maxLenException)
            {
                throw new OnionSaRepositoryException($"Algum dos atributos do objeto ultrapassou a quantidade máxima de caracteres. Valide os dados inseridos e tente novamente.\nMais informações: {maxLenException.Message}");
            }
            catch (Exception ex)
            {
                throw new OnionSaRepositoryException($"Um erro ocorreu algo tentar inserir o cliente. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }
        }
        
        public async void InserirVariosClientes<T>(List<T> clientes) where T : Cliente
        {
            try
            {
                _dbSet.AddRange(clientes);
            }
            catch (UniqueConstraintException nullException)
            {
                throw new OnionSaRepositoryException($"O número de cliente que você está tentando inserir já está em uso. Valide os dados inseridos e tente novamente.\nMais informações: {nullException.Message}");
            }
            catch (CannotInsertNullException nullException)
            {
                throw new OnionSaRepositoryException($"Algum dos atributos obrigatórios do objeto foi enviado nulo ou vazio. Valide os dados inseridos e tente novamente.\nMais informações: {nullException.Message}");
            }
            catch (MaxLengthExceededException maxLenException)
            {
                throw new OnionSaRepositoryException($"Algum dos atributos do objeto ultrapassou a quantidade máxima de caracteres. Valide os dados inseridos e tente novamente.\nMais informações: {maxLenException.Message}");
            }
            catch (Exception ex)
            {
                throw new OnionSaRepositoryException($"Um erro ocorreu algo tentar inserir o cliente. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }
        }
        
        public async Task<Cliente> ObterClientePorDoc(string documento)
        {
            try
            {
                var cliente = await _dbSet.FirstOrDefaultAsync(x => x.CPFCNPJ == documento);
                return cliente;
            }
            catch (Exception ex)
            {
                throw new OnionSaRepositoryException($"Um erro ocorreu algo tentar obter o cliente. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }
        }
        
        public async Task<List<Cliente>> ObterTodosOsClientes()
        {
            try
            {
                var clientes = await _dbSet.ToListAsync();
                return clientes;
            }
            catch (Exception ex)
            {
                throw new OnionSaRepositoryException($"Um erro ocorreu algo tentar obter todos os clientes. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }
        }
        public void RemoverCliente<T>(T cliente) where T : Cliente
        {
            try
            {
                _dbSet.Remove(cliente);
            }
            catch (Exception ex)
            {
                throw new OnionSaRepositoryException($"Um erro ocorreu algo tentar remover o cliente. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }
        }
    }
}
