import React, { useEffect, useState } from 'react';
import Grafico from '../Components/Grafico';
import NavBar from '../Components/NavBar';
import PedidosService from '../Services/PedidosService';
import "../Styles/DashboardStyle.css"

function Dashboard() {
  const [pedidos, setPedidos] = useState([]);
  const [porRegiao, setPorRegiao] = useState([])
  const [porProduto, setPorProduto] = useState([])

  useEffect(() => {
    const fetchData = async () => {
      try {
        const pedidosData = await PedidosService.RetornaPedidos();
        const pedidosPorRegiao = await PedidosService.RetornaPorRegiao(pedidosData)
        const pedidosPorProduto = await PedidosService.RetornaPorProduto(pedidosData);
        console.log(pedidosData);
        setPorProduto(pedidosPorProduto);
        setPorRegiao(pedidosPorRegiao);
        setPedidos(pedidosData);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

    fetchData();
  }, []); 

  return (
    <>
      <NavBar Tela="dashboard" />

          <div className="row container mt-3">
              <h1>Dashboard</h1>
              <p>No Dashboard você irá poder visualizar os dados informativos sobre os seus pedidos, como a lista de todos os pedidos e os gráficos de vendas por região e vendas por produto.</p>
              <hr />
              <div className='row tabela' height="100px">
                  <h4>Lista de pedidos</h4>
                  <table className="table overflow-auto" width="100px">
                      <thead>
                          <tr>
                              <th scope="col">Razão Social</th>
                              <th scope="col">Produto</th>
                              <th scope="col">Valor final</th>
                              <th scope="col">Data de entrega</th>
                          </tr>
                      </thead>
                      <tbody>
                          {pedidos.map(pedido => (
                              <tr key={pedido.numeroDoPedido}>
                                  <td scope="row">{pedido.razaoSocial}</td>
                                  <td>{pedido.produto}</td>
                                  <td>{pedido.valorFinal}</td>
                                  <td>{pedido.dataEntrega}</td>
                              </tr>
                          ))}
                      </tbody>
                  </table>
              </div>
              <div className='row graficos'>
                  <div className='col-6 '>
                      <h4><b>Grafico de pedidos por região:</b></h4>
                      <Grafico Data={porRegiao} />
                  </div>
                  <div className='col-6'>
                      <h4><b>Grafico de pedidos por produto:</b></h4>
                      <Grafico Data={porProduto} />
                  </div>
              </div>

          </div>
    </>
  );
}

export default Dashboard;
