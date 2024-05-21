using OnionProjectHub.Domain.Models;


namespace OnionProjectHub.Repository.Interfaces
{
    public interface IPedidoRepository
    {
        void InserirPedido<T>(T pedido) where T : Pedido;
        void InserirVariosPedidos<T>(List<T> pedido) where T : Pedido;
        void AlterarPedido<T>(T pedido) where T : Pedido;
        void RemoverPedido<T>(T pedido) where T : Pedido;
        Task<Pedido> ObterPedidoPorNumero(int  numero);
        Task<List<Pedido>> ObterTodosOsPedidos();
        void Save();
    }
}
