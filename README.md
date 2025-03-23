# Sistema de Cadastro de Alunos

Este repositório contém o projeto de um **sistema de cadastro de alunos**, desenvolvido em **C#**. O sistema permite o cadastro de alunos, armazenamento de dados em um banco de dados local e captura de informações de endereço via API de CEP.

## Descrição do Projeto

O objetivo do projeto é criar um sistema simples de cadastro de alunos ou usuários, onde o usuário pode adicionar novos alunos, registrar suas informações e utilizar uma API externa para capturar automaticamente o endereço com base no CEP informado. As informações dos alunos são armazenadas em um banco de dados SQLServer, garantindo persistência dos dados.

### Funcionalidades

- **Cadastro de alunos**: Permite que os alunos sejam cadastrados no sistema, com informações como nome, e-mail, telefone, data de nascimento, endereço e outras informações importantes.
- **Captura de endereço via API de CEP**: Ao informar o CEP, o sistema faz uma requisição para uma API externa para preencher automaticamente os campos de endereço (rua, bairro, cidade, estado).
- **Armazenamento em banco de dados**: As informações dos alunos são armazenadas em um banco de dados local (SQLServer), permitindo o gerenciamento e persistência dos dados.
- **Listagem de alunos**: O sistema permite visualizar a lista de alunos cadastrados e encontrar alunos pelo seu ID de cadastro.

## Tecnologias Utilizadas

- **Linguagem**: C#
- **Framework**: .NET 6 ou superior
- **Banco de Dados**: SQL Server (pode ser substituído por outro, como MySQL ou SQLite, caso preferido)
- **API de CEP**: Via API pública como [ViaCEP](https://viacep.com.br/)
- Dapper, Injeção de Dependência e chamadas HTTP

## Estrutura do Projeto

O projeto é estruturado da seguinte maneira:

- **Models:** Contém as classes que representam os dados da aplicação, como Aluno e Endereco.
- **DAL (Data Access Layer):** Responsável pela interação com o banco de dados. Inclui o contexto de dados e as operações de leitura.
- **Interfaces:** Define os contratos que as implementações de serviços devem seguir. Esse padrão promove a separação de responsabilidades e facilita a injeção de dependência.
- **Services:** Contém a lógica de negócios, implementando as interfaces e realizando a comunicação com a API de CEP e manipulando os dados. Essa camada interage diretamente com o DAL para executar operações de banco de dados.
- **Controllers:** Responsáveis por processar as requisições da API. Eles utilizam os serviços para executar a lógica de negócios e retornar as respostas apropriadas.

## Como Usar

### 1. Clone o Repositório
Primeiro, clone o repositório para sua máquina local:
```bash
git clone https://github.com/gabyfachini/CadastroAlunos.git
```
### 2. Navegue até o Diretório do Projeto
### 3. Instale as Dependências
### 4. Configure o Banco de Dados
### 5. Execute o Projeto
