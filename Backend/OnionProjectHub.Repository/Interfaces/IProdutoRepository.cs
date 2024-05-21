using OnionProjectHub.Domain.Models;


namespace OnionProjectHub.Repository.Interfaces
{
    public interface IProdutoRepository
    {
        void InserirProduto<T>(T produto) where T : Produto;
        void AlterarProduto<T>(T produto) where T : Produto;
        void RemoverProduto<T>(T produto) where T : Produto;
        Task<Produto> ObterProdutoPorId(long  id);
        Task<Produto> ObterProdutoPorTitulo(string  npme);
        Task<List<Produto>> ObterTodosOsProdutos();
    }
}
