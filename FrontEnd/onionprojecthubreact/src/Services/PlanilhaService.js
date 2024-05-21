import * as XLSX from 'xlsx';

class PlanilhaService {
  async EnviaPlanilha(planilha) {
    return new Promise(async (resolve, reject) => {
        const objetoFormData = new FormData();
        objetoFormData.append('planilha', planilha, 'arquivo.csv');
    
        try {
          const response = await fetch('https://localhost:7254/onionsa/Pedidos/enviar-planilha', {
            method: 'POST',
            body: objetoFormData,
          });
          if (await response.ok) {
            resolve(response.ok);
          } else {
            reject(response.text().then(text => { throw new Error(text) }) )         }
        } catch (error) {
            reject(error);
            throw error;
            console.error('Error sending request:', error);
            console.error('Response:', await error.json());    }
    })

  }

  async ConvertePlanilha(planilha) {
    return new Promise((resolve, reject) => {
      if (planilha) {
        const reader = new FileReader();
  
        reader.onload = (e) => {
          try {
            const data = new Uint8Array(e.target.result);
            const workbook = XLSX.read(data, { type: 'array' });

            const sheetName = workbook.SheetNames[0];
            const sheet = workbook.Sheets[sheetName];

            const csvData = XLSX.utils.sheet_to_csv(sheet,{ blankrows: false });
            const blob = new Blob([csvData], { type: 'text/csv' });
  
            resolve(blob);
          } catch (error) {
            console.error('Error converting spreadsheet:', error);
            reject(error);
          }
        };
  
        reader.readAsArrayBuffer(planilha);
      }
    });
  }

  async SalvarPlanilha(planilha) {
    return new Promise(async (resolve, reject) => {
        try {

            const csvData = await this.ConvertePlanilha(planilha);
      
            // Use csvData conforme necessário, por exemplo, enviar para o servidor
            await this.EnviaPlanilha(csvData).then((result) => {
                resolve(result)
            }).catch((error) => {
                reject(error);
            })
            resolve();
          } catch (error) {
            console.error('Error saving spreadsheet:', error);
            reject(error);
            throw error;

          }
    });

  }
}

export default new PlanilhaService();
