import { Component } from "react";
import NavBar from "../Components/NavBar";
import React from "react";
import PlanilhaService from "../Services/PlanilhaService";
import Swal from 'sweetalert2';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTable } from '@fortawesome/free-solid-svg-icons';
import planilhaEx from "../Assets/planilhaEx.xlsx"
import "../Styles/HomePageStyle.css"
class HomePage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      planilha: null,
      hasError: false,
      error: null,
    };
  }

  componentDidCatch(error, errorInfo) {
    this.setState({
      hasError: true,
      error,
    });
  }
    handleFileChange = (event) => {
        const planilha = event.target.files[0];
        if (planilha) {
          // Atualize o estado para incluir a informação sobre a planilha
          this.setState({ planilha });
        }
      };

      handleClick = async () => {
        const { planilha } = this.state;
        // Exibe o SweetAlert enquanto aguarda a conclusão do método
        Swal.fire({
          title: 'Processando...',
          showLoading: true,
          allowOutsideClick: false,
          onBeforeOpen: () => {
            Swal.showLoading();
          },
        });
    
        // try {
          // Chama o método na classe de serviço
          const resultado = PlanilhaService.SalvarPlanilha(planilha).then((result) => {
            Swal.fire({
              icon: 'success',
              title: 'Concluído!',
              text: 'A planilha foi processada com sucesso.',
            });
          }).catch((e) =>
          {
            e.catch(error => 
            {
              Swal.fire({
                icon: 'error',
                title: 'Erro!',
                text: error.message,
              });
            });

          });
      };
    
    render()
    {
        const { planilha } = this.state;
        return (
            <>
                <NavBar  Tela="home"/>

                <div className=" row flex-grow-1 divPrincipal">
                    <div className="row texto" >
                        <h1 className="onionSaSmall">Onion Project - Passo a passo de uso...!</h1>
                    </div>
                    <div>
                      <ol className="row texto">
                        <ul className="onionSaSmall">Primeiro você precisará que sua planilha possua os seguintes campos exatamente igual a essa:</ul>
                        <ul className="onionSaSmall"><a href={planilhaEx} download="planilha-exemplo" target="_blank">Planilha de exemplo</a></ul>
                        <ul className="onionSaSmall">Em seguida, você irá clicar em <b>"Escolher arquivo"</b> para selecionar sua planilha.</ul>
                      </ol>
                    </div>
                    <div className="row texto">
                      <label for="inputPlanilha" className="form-label">Insira aqui a sua planilha</label>
                      <input className="form-control" type="file" id="inputPlanilha" accept=".xls, .xlsx, .csv" onChange={this.handleFileChange}/>
                    </div>
                    {planilha ?
                        <div className="row texto">
                          <p >A planilha <b>{planilha ? planilha.name : ''}</b> foi selecionada!</p>
                          <button className="btn btn-dark" onClick={this.handleClick}>Enviar planilha</button>
                        </div>
                    : null}
                </div>

            </>
        );
    }
}

export default HomePage;