using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using OnionProjectHub.Domain.Models;
using OnionProjectHub.Repository.Context;
using OnionProjectHub.Repository.Exceptions;
using OnionProjectHub.Repository.Interfaces;


namespace OnionProjectHub.Repository.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly OnionSaContext _cntxt;
        private readonly DbSet<Pedido> _dbSet;
        public PedidoRepository(OnionSaContext context)
        {
            _cntxt = context ?? throw new ArgumentNullException(nameof(context)); ;
            _dbSet = _cntxt.Set<Pedido>();
        }
        public void AlterarPedido<T>(T pedido) where T : Pedido
        {
            try
            {
                _dbSet.Update(pedido);
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
                throw new OnionSaRepositoryException($"Um erro ocorreu algo tentar atualizar o pedido. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }

        }
        public async void InserirPedido<T>(T pedido) where T : Pedido
        {
            try
            {
                await _dbSet.AddAsync(pedido);
            }
            catch (UniqueConstraintException nullException)
            {
                throw new OnionSaRepositoryException($"O número de pedido que você está tentando inserir já está em uso. Valide os dados inseridos e tente novamente.\nMais informações: {nullException.Message}");
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
                throw new OnionSaRepositoryException($"Um erro ocorreu algo tentar inserir o pedido. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }
        }
        public  void InserirVariosPedidos<T>(List<T> pedidos) where T : Pedido
        {
            try
            {
                _dbSet.AddRange(pedidos);
            }
            catch (UniqueConstraintException nullException)
            {
                throw new OnionSaRepositoryException($"O número de pedido que você está tentando inserir já está em uso. Valide os dados inseridos e tente novamente.\nMais informações: {nullException.Message}");
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
                throw new OnionSaRepositoryException($"Um erro ocorreu algo tentar inserir o pedido. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }
        }
        public async Task<Pedido> ObterPedidoPorNumero(int numero)
        {
            try
            {
                var pedido = await _dbSet.FirstOrDefaultAsync(x => x.NumeroDoPedido == numero);
                return pedido;
            }
            catch (Exception ex)
            {
                throw new OnionSaRepositoryException($"Um erro ocorreu algo tentar obter o pedido. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }
        }
        public async Task<List<Pedido>> ObterTodosOsPedidos()
        {
            try
            {
                var pedidos = await _dbSet.ToListAsync();
                return pedidos;
            }
            catch (Exception ex)
            {
                throw new OnionSaRepositoryException($"Um erro ocorreu algo tentar obter todos os pedidos. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }
        }
        public void RemoverPedido<T>(T pedido) where T : Pedido
        {
            try
            {
                _dbSet.Remove(pedido);
            }
            catch (Exception ex)
            {
                throw new OnionSaRepositoryException($"Um erro ocorreu algo tentar remover o pedido. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }
        }
        public void Save()
        {
            try
            {
                _cntxt.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new OnionSaRepositoryException($"Um erro ocorreu algo tentar remover o pedido. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }
        }




    }
}
