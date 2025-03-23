# Sistema de Cadastro de Alunos - Academia

Este repositório contém o projeto de um **sistema de cadastro de alunos de uma academia**, desenvolvido em **C#**. O sistema permite o cadastro de alunos, armazenamento de dados em um banco de dados e captura de informações de endereço via API de CEP.

## Descrição do Projeto

O objetivo do projeto é criar um sistema simples de cadastro de alunos de uma academia, onde o usuário pode adicionar novos alunos, registrar suas informações e utilizar uma API externa para capturar automaticamente o endereço com base no CEP informado. As informações dos alunos são armazenadas em um banco de dados, garantindo persistência dos dados.

### Funcionalidades

- **Cadastro de alunos**: Permite que os alunos sejam cadastrados no sistema, com informações como nome, CPF, e-mail, telefone, data de nascimento, e endereço.
- **Captura de endereço via API de CEP**: Ao informar o CEP, o sistema faz uma requisição para uma API externa para preencher automaticamente os campos de endereço (rua, bairro, cidade, estado).
- **Armazenamento em banco de dados**: As informações dos alunos são armazenadas em um banco de dados SQL, permitindo o gerenciamento e persistência dos dados.
- **Listagem de alunos**: O sistema permite visualizar a lista de alunos cadastrados.

## Tecnologias Utilizadas

- **Linguagem**: C#
- **Framework**: .NET 6 ou superior
- **Banco de Dados**: SQL Server (pode ser substituído por outro, como MySQL ou SQLite, caso preferido)
- **API de CEP**: Via API pública como [ViaCEP](https://viacep.com.br/) ou [Postmon](http://postmon.com.br/)
- **ORM**: Entity Framework Core

## Estrutura do Projeto

O projeto é estruturado da seguinte maneira:

- **Models**: Contém as classes de modelo de dados, como a classe `Aluno` e `Endereco`.
- **Services**: Contém a lógica de negócios, como a comunicação com a API de CEP e manipulação de dados do banco de dados.
- **Controllers**: Controladores responsáveis por processar as requisições da API.
- **Data**: Contém o contexto do banco de dados e a configuração de Entity Framework.
- **Views**: Caso haja uma interface gráfica (por exemplo, usando Windows Forms ou ASP.NET), as views serão responsáveis por exibir os dados ao usuário.

## Como Usar

### 1. Clone o Repositório

Primeiro, clone o repositório para sua máquina local:

```bash
git clone https://github.com/seu-usuario/sistema-cadastro-alunos.git
