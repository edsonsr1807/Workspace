// Robo criado ultilizando a blibioteca puppeteer junto com nodejs
// Finalidade de acessar alguma pagina web e recuperar dados(web scraping)

const puppeteer = require('puppeteer');
const readlineSync = require('readline-sync'); // Blibioteca para console realizar perguntas

/*(async () => {
  
})();*/  // Função assicrona

console.log("Teste de Robo");
robo(); // chamando função robo  

// Por ter promesas a função é definida como async function
async function robo(){
    const browser = await puppeteer.launch( {}); // Comando para armezar abertura do nav
    const page = await browser.newPage(); // Abertura de uma nova pagina
    const moedaBase =  readlineSync.question("Informe uma Moeda Base: ") ||'dolar';
    const moedaFinal = readlineSync.question("Informe uma Moeda Final: ") ||'real';

    // `` uso de acento para add var na string URL
    const qualquerurl = `https://www.google.com/search?q=${moedaBase}+para+${moedaFinal}&ei=-Fq6YKbpENq45OUPk9eW0AU&oq=${moedaBase}+para&gs_lcp=Cgdnd3Mtd2l6EAMYADIKCAAQsQMQRhCCAjIFCAAQsQMyBQgAELEDMgIIADIICAAQsQMQgwEyBQgAELEDMgIIADIFCAAQyQMyAggAMgIIADoLCC4QsQMQxwEQowI6BAgAEEM6CgguEMcBEKMCEEM6BwgAELEDEEM6DQguELEDEMcBEKMCEEM6CggAELEDEIMBEEM6DwgAELEDEIMBEEMQRhCCAlDDCFjILGCDN2gAcAJ4AIAB1AGIAeQIkgEGMTAuMC4xmAEAoAEBqgEHZ3dzLXdpesABAQ&sclient=gws-wiz`;
    
    await page.goto(qualquerurl); // URL da pagina que irá abrir

    const resultado = await page.evaluate(() => {
        return document.querySelector('.a61j6.vk_gy.vk_sh.Hg3mWc').value; // Função para selecionar o campo da conversão atravé do console do nav
      });
    
    console.log(`O valor 1 ${moedaBase} em ${moedaFinal} é ${resultado}`);
    
    //await page.screenshot({ path: 'example.png' }); // Comando para tirar screenshot
  
    await browser.close();
}



