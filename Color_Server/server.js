//Importa o módulo express
const express = require('express');
//Importa o módulo cors
// const cors = require('cors');
//Instância o express
const app = express();
//Crio qual vai ser a porta que o servidor será exibido
const port = 3001;

//
// app.use(cors());

app.get('/random-color', (req, res) => {
  const randomColor = getRandomColor();
  res.json({ color: randomColor });
});

// Função para selecionar a cor
function getRandomColor() {
  //Váriavel com todas as possibilidades
  const letters = '0123456789ABCDEF';
  //Váriavel para adicionar o # tornando a cor hexadecimal
  let color = '#';
  //Laço de repetição, que vai selecionar 6 números para colocar a cor aleatória 
  for (let i = 0; i < 6; i++) {
    //Adiciona um dos valores da váriavel letters, a partir do indice, pegando um valor de 0 a 1 e multiplicando por 16 depois arredondando esse valor de 0 até 15. 
    color += letters[Math.floor(Math.random() * 16)];
  }
  //Retorna a váriavel color, contendo a cor randomizada.
  return color;
}

//Inicia o servidor, depois de passar a porta e uma fução de retorno.
app.listen(port, () => {
  console.log(`Server is running on port ${port}`);
});
