# API Carros

Pequeno projeto desenvolvido para estudo da plataforma ASP.NET.

## Model

Está presente nesta pasta a classe modelo, onde as propriedades se referem aos campos do banco de dados, permitindo criar um objeto para cada carro inserido no banco de dados.

## Database

Está presente nessa classe as operações com banco de dados:

- Lista de todos os carros
- Adicionar carro
- Excluir carro
- Buscar por um carro
- Atualizar um carro

## Controller

O controller é responsável por controlar (dã) as chamadas feitas pelo cliente, direcionando, segundo o verbo HTTP e os parâmetros passados na url, a solicitação para realizar a devida operação.

### Verbos e ações suportadas:

- **GET** → retorna todos os carros presentes no banco de dados
- **GET**/placa → Realiza uma busca no banco de dados por um determinado carro através da placa, retorna o carro buscado ou uma lista vazia se não for encontrado.
- **POST** → Adiciona um carro ao banco de dados e retorna o carro adicionado.
- **PUT** → Atualiza um determinado carro, através da placa. Se esse carro não estiver presente, retornará vazio.
- **DELETE**/placa → Exclui do banco de dados o carro cuja a placa foi passada na url, retorna o carro excluído indicando sucesso na operação.
