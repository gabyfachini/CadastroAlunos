# Sistema de Registro de Alunos

## Visão Geral

Este é um **Sistema de Registro de Alunos** desenvolvido em C# utilizando uma arquitetura limpa com injeção de dependência. O sistema permite cadastrar, listar, buscar, atualizar e excluir logicamente os registros de alunos. Também integra a API **ViaCep** para buscar automaticamente informações de endereço brasileiras a partir do CEP.

---

## Funcionalidades

- Cadastrar novos alunos com validações de entrada (nome, sobrenome, data de nascimento, sexo, email, telefone e endereço).
- Listar todos os alunos cadastrados.
- Buscar um aluno pelo ID.
- Atualizar dados do aluno (função a ser implementada).
- Exclusão lógica (soft delete), marcando o aluno como inativo.
- Busca automática de endereço via API ViaCep.
- Validações de email, telefone, data e outros campos usando expressões regulares.
- Injeção de dependência para melhor modularidade e testabilidade.

---

## Tecnologias Utilizadas

- .NET / C#
- Injeção de Dependência com Microsoft.Extensions.DependencyInjection
- HttpClient para requisições externas (ViaCep)
- Serialização/Deserialização JSON
- Expressões regulares para validação de entrada
- Aplicação console para interação com o usuário

---

## Como Usar

1. Execute o programa.
2. Utilize o menu para escolher a operação desejada:
   - Cadastrar um novo aluno.
   - Listar todos os alunos.
   - Buscar um aluno pelo ID.
   - Atualizar dados do aluno (em breve).
   - Excluir aluno logicamente.
   - Sair do programa.

3. Siga as instruções para inserir os dados ao cadastrar ou buscar alunos.

---

## Estrutura do Projeto

- **Models**: Contém os modelos de aluno e endereço.
- **Services**: Lógica de negócio, como serviço de aluno e consulta do ViaCep.
- **Repositories**: Camada de acesso a dados para armazenamento e recuperação.
- **Presentation**: Lógica da interface console para interação com o usuário.
- **Program**: Ponto de entrada da aplicação, configuração da injeção de dependência e loop do menu principal.

---

## Observações

- A função de atualizar aluno está planejada, mas ainda não implementada.
- O sistema considera o formato de CEP brasileiro.
- A exclusão lógica apenas marca o aluno como inativo, sem remover os dados.
- O projeto usa chamadas assíncronas para as requisições externas.

---

## Licença

Este projeto é open source e livre para uso.

---

## Autor

Desenvolvido por Gabryella Fachini.
