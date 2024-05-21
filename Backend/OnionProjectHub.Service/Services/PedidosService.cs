using Newtonsoft.Json;
using OnionProjectHub.Domain.Models;
using OnionProjectHub.Repository.Interfaces;
using System.Data;
using OnionProjectHub.Service.Exceptions;
using OnionProjectHub.Service.Validations;

namespace OnionProjectHub.Service.Services
{
    public class PedidoService
    {
        private readonly IPedidoRepository _repo;
        private readonly PedidoValidation PedidoValidation;
        public PedidoService(IPedidoRepository repo)
        {
            _repo = repo;
            PedidoValidation = new PedidoValidation();
        }
        public Pedido CriaObjetoPedido(DataRow linha)
        {
            try
            {
                Pedido pedido = new Pedido()
                {
                    NumeroDoPedido = int.Parse(linha[4].ToString()),
                    DocumentoCliente = linha[0].ToString(),
                    Cep = linha[2].ToString(),
                    Data = DateTime.Parse(linha[5].ToString()),
                };

                
                return pedido;
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
        public void AdicionaPedido(Pedido pedido)
        {
            try
            {
                PedidoValidation.ValidaObjetoPedido(pedido);
                _repo.InserirPedido(pedido);
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
        public void AdicionaVariosPedidos(List<Pedido> pedidos)
        {
            try
            {
                PedidoValidation.ValidaListaPedidos(pedidos);
                _repo.InserirVariosPedidos(pedidos);
            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar inserir a lista de pedidos. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }
        
        public void AlteraPedido(Pedido pedido)
        {
            try
            {
                PedidoValidation.ValidaObjetoPedido(pedido);
                _repo.AlterarPedido(pedido);

            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar alterar o pedido. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }
        public async void RemovePedidoPorNumero(int numero)
        {
            try
            {
                var pedido = await _repo.ObterPedidoPorNumero(numero);

                PedidoValidation.ValidaObjetoPedido(pedido);

                _repo.RemoverPedido(pedido);

            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar remover o pedido. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }
        public async Task<Pedido> ObtemPedidoPorNumero(int numero)
        {
            try
            {
                var pedido = await _repo.ObterPedidoPorNumero(numero);

                PedidoValidation.ValidaObjetoPedido(pedido);

                return pedido;

            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar obter o pedido. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }
        public async Task<List<Pedido>> ObtemTodosOsPedidos()
        {
            try
            {
                var pedidos = await _repo.ObterTodosOsPedidos();

                PedidoValidation.ValidaListaDePedidos(pedidos);

                return pedidos;

            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao tentar obter todos os Pedidos. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }
        public async Task<DadosCep> RetornaDadosDoCep(string cep) 
        {
            DadosCep dadosCep = null;
            try
            {
                string urlCep = $"https://opencep.com/v1/{cep}.json";

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(urlCep);
                    if(response.IsSuccessStatusCode)
                    {
                        var conteudo = await response.Content.ReadAsStringAsync();
                        dadosCep = JsonConvert.DeserializeObject<DadosCep>(conteudo);
                    }

                    return dadosCep;
                }
            }
            catch (Exception)
            {
                try
                {
                    string urlCep = $"https://viacep.com.br/ws/{cep}/json/";

                    using (HttpClient client = new HttpClient())
                    {
                        var response = await client.GetAsync(urlCep);
                        if (response.IsSuccessStatusCode)
                        {
                            var conteudo = await response.Content.ReadAsStringAsync();
                            dadosCep = JsonConvert.DeserializeObject<DadosCep>(conteudo);
                        }

                        return dadosCep;
                    }
                }
                catch (Exception ex)
                {

                    throw new OnionSaServiceException($"Ocorreu um erro ao obter os detalhes do cep. Revise os dados enviados ou entre em contato com a equipe de suporte da Onion S.A e tente novamente.\nMais detalhes: {ex.Message}.");
                }

            }

 
        }
        public async Task<string> IdentificaRegiaoPedido(string uf) 
        {
            #region Listas com as regiões
            // Estados da região Norte
            List<string> estadosNorte = new List<string> { "AC", "AP", "AM", "PA", "RO", "RR", "TO" };

            // Estados da região Nordeste
            List<string> estadosNordeste = new List<string> { "AL", "BA", "CE", "MA", "PB", "PE", "PI", "RN", "SE" };

            // Estados da região Centro-Oeste
            List<string> estadosCentroOeste = new List<string> { "DF", "GO", "MT", "MS" };

            // Estados da região Sudeste
            List<string> estadosSudeste = new List<string> { "ES", "MG", "RJ", "SP" };

            // Estados da região Sul
            List<string> estadosSul = new List<string> { "PR", "RS", "SC" };
            #endregion Listas com as regiões

            try
            {
                if (estadosSudeste.Any(e => e == uf))
                {
                    return "Sudeste";
                }
                else if (estadosSul.Any(e => e == uf))
                {
                    return "Sul";
                }else if (estadosCentroOeste.Any(e => e == uf))
                {
                    return "CentroOeste";
                }
                else if (estadosNorte.Any(e => e == uf))
                {
                    return "Norte";

                }
                else if (estadosNordeste.Any(e => e == uf))
                {
                    return "Nordeste";
                }
                else
                {
                    throw new OnionSaServiceException("Não foi possivel localizar o UF do CEP disponibilizado. Valide se o CEP foi inserido corretamente ou entre em contato com a equipe de suporte da Onion S.A e tente novamente.");
                }
            }
            catch (OnionSaServiceException onionExc)
            {
                throw onionExc;
            }

            catch (Exception ex)
            {

                throw new OnionSaServiceException($"Ocorreu um erro ao tentar identificar a região do CEP. Revise os dados inseridos ou entre em contato com a equipe de suporte da Onion S.A e tente novamente.\nMais detalhes: {ex.Message}.");
            }

        }
        public async Task<double> CalculaPrecoFinal(Pedido pedido, string regiao) 
        {

            double precoFinal = 0;
            double taxaFreteSudeste = 0.1;
            double taxaFreteNorteNordeste = 0.3;
            double taxaFreteSulCentro = 0.2;

            try
            {
                if(regiao == "Sudeste" )
                {
                    precoFinal = pedido.Produto.Preco + (taxaFreteSudeste * pedido.Produto.Preco);
                }
                else if(regiao == "Sul" ||  regiao == "CentroOeste")
                {
                    precoFinal = pedido.Produto.Preco + (taxaFreteSulCentro * pedido.Produto.Preco);
                }
                else if(regiao == "Norte" || regiao == "Nordeste")
                {
                    precoFinal = pedido.Produto.Preco + (taxaFreteNorteNordeste * pedido.Produto.Preco);
                }
                return precoFinal;

            }
            catch (Exception ex)
            {

                throw new OnionSaServiceException($"Ocorreu um erro ao calcular o valor final do pedido. Revise os dados inseridos ou entre em contato com a equipe de suporte da Onion S.A e tente novamente.\nMais detalhes: {ex.Message}.");
            }

        }
        public async Task<DateTime> CalculoDataDeEntrega(DateTime dataDoPedido, string regiao)
        {
            int prazoNorteNordeste = 10;
            int prazoCentrSul = 5;
            int prazoSudeste = 1;

            DateTime dataEntrega = dataDoPedido;
            try
            {
                if (regiao == "Sudeste")
                {
                    dataEntrega = DefineData(dataDoPedido, prazoSudeste);
                }
                else if (regiao == "Sul" || regiao == "CentroOeste")
                {
                    dataEntrega = DefineData(dataDoPedido, prazoCentrSul);
                }
                else if (regiao == "Norte" || regiao == "Nordeste")
                {
                    dataEntrega = DefineData(dataDoPedido, prazoNorteNordeste);
                }
                return dataEntrega;
            }
            catch(OnionSaServiceException onionExc)
            {
                throw onionExc;
            }
            catch (Exception ex)
            {

                throw new OnionSaServiceException($"Ocorreu um erro ao tentar definir a data de entrega do pedido; Valide os dados inseridos ou entre em contato com o suporte da Onion S.A e tente novamente.\nMais detalhes: {ex.Message}");
            }
        }
        private DateTime DefineData(DateTime data, int dias) 
        {
            int i = 1;
            DateTime dataEntrega = data;
            try
            {
                while (i < dias)
                {
                    dataEntrega = dataEntrega.AddDays(1);
                    if (!VerificaDiaUtil(dataEntrega)) i ++;
                }

                return dataEntrega;
            }
            catch (Exception ex)
            {

                throw new OnionSaServiceException($"Ocorreu um erro ao tentar definir a data de entrega do pedido. Valide os dados inseridos ou entre em contato com o suporte da Onion S.A e tente novamente.\nMais detalhes: {ex.Message}");
            }
        }
        private bool VerificaDiaUtil(DateTime data)
        {
            return data.DayOfWeek == DayOfWeek.Sunday || data.DayOfWeek == DayOfWeek.Saturday;
        }
        public void SalvaAlteracoes()
        {
            try
            {
                _repo.Save();

            }
            catch (OnionSaServiceException onionExcp)
            {
                throw onionExcp;
            }
            catch (Exception ex)
            {
                throw new OnionSaServiceException($"Ocorreu um erro ao salvar as alterações. Revise os dados enviados e tente novamente.\nMais detalhes:{ex.Message}");
            }
        }
    }

}
