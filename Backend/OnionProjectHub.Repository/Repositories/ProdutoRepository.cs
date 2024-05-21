using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using OnionProjectHub.Domain.Models;
using OnionProjectHub.Repository.Context;
using OnionProjectHub.Repository.Exceptions;
using OnionProjectHub.Repository.Interfaces;


namespace OnionProjectHub.Repository.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly OnionSaContext _cntxt;
        private readonly DbSet<Produto> _dbSet;
        public ProdutoRepository(OnionSaContext context)
        {
            _cntxt = context ?? throw new ArgumentNullException(nameof(context)); ;
            _dbSet = _cntxt.Set<Produto>();
        }
        
        public void AlterarProduto<T>(T produto) where T : Produto
        {
            try
            {
                _dbSet.Update(produto);
                _cntxt.SaveChanges();
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
                throw new OnionSaRepositoryException($"Um erro ocorreu algo tentar atualizar o produto. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }

        }
        public async void InserirProduto<T>(T produto) where T : Produto
        {
            try
            {
                await _dbSet.AddAsync(produto);
                await _cntxt.SaveChangesAsync();
            }
            catch (UniqueConstraintException nullException)
            {
                throw new OnionSaRepositoryException($"O número de produto que você está tentando inserir já está em uso. Valide os dados inseridos e tente novamente.\nMais informações: {nullException.Message}");
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
                throw new OnionSaRepositoryException($"Um erro ocorreu algo tentar inserir o produto. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }
        }
        public async Task<Produto> ObterProdutoPorId(long id)
        {
            try
            {
                var produto = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
                return produto;
            }
            catch (Exception ex)
            {
                throw new OnionSaRepositoryException($"Um erro ocorreu ao tentar obter o produto. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }
        }
        
        public async Task<Produto> ObterProdutoPorTitulo(string nome)
        {
            try
            {
                var produto = await _dbSet.FirstOrDefaultAsync(x => x.Nome == nome);
                return produto;
            }
            catch (Exception ex)
            {
                throw new OnionSaRepositoryException($"Um erro ocorreu ao tentar obter o produto. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }
        }
        public async Task<List<Produto>> ObterTodosOsProdutos()
        {
            try
            {
                var produtos = await _dbSet.ToListAsync();
                return produtos;
            }
            catch (Exception ex)
            {
                throw new OnionSaRepositoryException($"Um erro ocorreu ao tentar obter todos os produtos. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }
        }
        public void RemoverProduto<T>(T produto) where T : Produto
        {
            try
            {
                _dbSet.Remove(produto);
                _cntxt.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new OnionSaRepositoryException($"Um erro ocorreu ao tentar obter o produto. Valide os dados inseridos e tente novamente.\nMais informações: {ex.Message}");
            }
        }
    }
}
