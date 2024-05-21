using Microsoft.AspNetCore.Mvc;
using OnionProjectHub.Domain.Models;
using OnionProjectHub.Repository.Interfaces;
using OnionProjectHub.Service.Exceptions;
using OnionProjectHub.Service.Services;
using OnionProjectHub.Service.Validations;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnionProjectHub.API.Controllers
{
    [ApiController]
    [Route("onionsa/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoRepository _repo;
        private readonly PedidoService _service;
        private readonly ClienteService _clienteService;
        private readonly ProdutoService _produtoService;
        public PedidosController(IPedidoRepository repo, IClienteRepository clienteRepo, IProdutoRepository produtoRepo)
        {
            _repo = repo;
            _service = new PedidoService(repo);
            _clienteService = new ClienteService(clienteRepo);
            _produtoService = new ProdutoService(produtoRepo);

        }

        [HttpPost("enviar-planilha")]
        public async Task<IActionResult> EnviarPlanilha(IFormFile planilha)
        {
            CSVService csvService = new CSVService();
            CSVValidation csvValidation = new CSVValidation();
            PedidoValidation pedidoValidation = new PedidoValidation();
            ClienteValidation clienteValidation = new ClienteValidation();
            List<Cliente> clientes = new List<Cliente>();
            List<Pedido> pedidos = new List<Pedido>();
            try
            {
                DataTable dt = await csvService.TransformaCSVParaDataTable(planilha);
                foreach(DataRow linha in dt.Rows)
                {
                    var novaLinha = csvService.TrataCamposLinha(linha);
                    novaLinha.AcceptChanges();
                    csvValidation.ValidaLinhaDataTable(novaLinha, dt.Rows.IndexOf(linha));

                    var cliente = (_clienteService.CriaObjetoCliente(novaLinha));
                    clienteValidation.ValidaObjetoCliente(cliente);
                    if(!(clientes.Any(a => a.CPFCNPJ == cliente.CPFCNPJ)))
                    {
                        clientes.Add(cliente);

                    }

                    var pedido = _service.CriaObjetoPedido(novaLinha);
                    pedido.Produto = await _produtoService.ObtemProdutoPorTitulo(novaLinha["Produto"].ToString());
                    pedido.ProdutoId = pedido.Produto.Id;
                    pedidoValidation.ValidaObjetoPedido(pedido);
                    pedidos.Add(pedido);

                }

                _clienteService.AdicionaVariosClientes(clientes);
                _service.AdicionaVariosPedidos(pedidos);

                _service.SalvaAlteracoes();

                return Ok();
                
            }
            catch (OnionSaServiceException onionExcp)
            {
                Console.WriteLine($"Exception: {onionExcp.Message}");

                return BadRequest(onionExcp.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");

                return BadRequest($"Ocorreu um erro ao tentar processar a planinha. Revise os dados enviados e tente novamente.");
            }
        }

       
        [HttpPost("obter-dados")]
        public async Task<IActionResult> ObtemDadosPlanilha()
        {

            PedidoValidation pedidoValidation = new PedidoValidation();
            List<Cliente> clientes = new List<Cliente>();
            List<Pedido> pedidos = new List<Pedido>();
            List<PedidoDTO> pedidosDTOs = new List<PedidoDTO>();
            try
            {
                pedidos = await _service.ObtemTodosOsPedidos();
                foreach(Pedido pedido in pedidos)
                {
                    pedidoValidation.ValidaObjetoPedido(pedido);

                    pedido.Produto = await _produtoService.ObtemProdutoPorId(pedido.ProdutoId);
                    pedido.Cliente = await _clienteService.ObtemClientePorDoc(pedido.DocumentoCliente);

                    PedidoDTO novoPedido = new PedidoDTO();

                    var dadosCep = await _service.RetornaDadosDoCep(pedido.Cep);

                    novoPedido.Regiao = await _service.IdentificaRegiaoPedido(dadosCep.UF);

                    novoPedido.ValorFinal = await _service.CalculaPrecoFinal(pedido, novoPedido.Regiao);

                    novoPedido.DataEntrega = await _service.CalculoDataDeEntrega(pedido.Data, novoPedido.Regiao);

                    novoPedido.CEP = pedido.Cep;
                    novoPedido.NumeroDoPedido = pedido.NumeroDoPedido;
                    novoPedido.Produto = pedido.Produto.Nome;
                    novoPedido.RazaoSocial = pedido.Cliente.RazaoSocial;
                    novoPedido.Documento = pedido.DocumentoCliente;
                    novoPedido.Valor = pedido.Produto.Preco;
                    novoPedido.Data = pedido.Data;

                    pedidosDTOs.Add(novoPedido);
                    
                }

                _service.SalvaAlteracoes();

                return Ok(pedidosDTOs);
                
            }
            catch (OnionSaServiceException onionExcp)
            {
                return BadRequest(onionExcp.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"N�o foi possivel retornar os dados da planilha. Revise os dados enviados e tente novamente.");
            }
        }
    }
}