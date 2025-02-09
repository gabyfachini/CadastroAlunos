using CadastroAlunos.DAL;
using CadastroAlunos.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroAlunos.Models
{
    public class AlunoPresentation

    {
        private readonly IAlunoService _alunoService;
        public AlunoPresentation(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        public void RegisterStudents()
        {
                /*var novoAluno = new Aluno();

                novoAluno.Nome = ObterEntradaValida("Digite o nome do aluno:", "Nome deve ter entre 2 e 100 caracteres e não pode ser vazio.");
                novoAluno.Sobrenome = ObterEntradaValida("Digite o sobrenome do aluno:", "Sobrenome deve ter entre 2 e 100 caracteres e não pode ser vazio.");
                novoAluno.Nascimento = ObterDataValida("Digite a data de nascimento (formato: YYYY-MM-DD):");
                novoAluno.Sexo = ObterSexoValido("Digite o sexo (M/F):");
                novoAluno.Email = ObterEmailValido("Digite o email:", "E-mail inválido ou o e-mail não pertence ao domínio 'exemplo.com'.");
                novoAluno.Telefone = ObterEntradaValida("Digite o telefone:", "Telefone não pode ser vazio.");

                novoAluno.DataDeAtualizacao = novoAluno.DataDeCadastro = DateTime.Now;
                novoAluno.Ativo = true;

                novoAluno = await ObterEnderecoPorCepAsync(novoAluno);

                if (ValidarAluno(novoAluno))
                {
                    string connectionString = _connectionString.GetConnectionString("Default");
                    CadastrarAluno(novoAluno, connectionString);

                    Console.WriteLine("Aluno cadastrado com sucesso!");
                }
                Console.Clear();*/
        }
        public void StudentList()
        {
            Console.Clear();
            Console.WriteLine("REGISTRO DE ALUNOS\n");

            List<Aluno> alunos = _alunoService.ListarAlunos();

            foreach (var aluno in alunos)
            {
                Console.WriteLine($"ID: {aluno.Id}\nNome: {aluno.Nome} {aluno.Sobrenome}\nNascimento: {aluno.Nascimento}\nSexo: {aluno.Sexo}\nEmail: {aluno.Email}\nTelefone: {aluno.Telefone}\nEndereço: {aluno.Logradouro}, {aluno.Complemento}.{aluno.Bairro}, {aluno.Localidade} - {aluno.UF} (CEP: {aluno.Cep})\n");

            }

            Console.WriteLine("Pressione qualquer tecla para voltar ao menu");
            Console.ReadKey();
            Console.Clear();
        }

        public void StudentSearch()
        {
            Console.WriteLine("BUSCA DE ALUNO\n");
            List<Aluno> alunos = _alunoService.ListarAlunos();

            Console.Write("Digite o ID do aluno: ");
            int alunoId;
            if (int.TryParse(Console.ReadLine(), out alunoId))
            {
                var aluno = alunos.FirstOrDefault(a => a.Id == alunoId);

                if (aluno != null)
                {
                    Console.WriteLine($"ID: {aluno.Id}\nNome: {aluno.Nome} {aluno.Sobrenome}\nNascimento: {aluno.Nascimento}\nSexo: {aluno.Sexo}\nEmail: {aluno.Email}\nTelefone: {aluno.Telefone}\nEndereço: {aluno.Logradouro}, {aluno.Complemento}.{aluno.Bairro}, {aluno.Localidade} - {aluno.UF} (CEP: {aluno.Cep})\n");
                
                }
                else
                {
                    Console.WriteLine("Aluno não encontrado!");
                }
            }
            else
            {
                Console.WriteLine("ID inválido. Tente novamente.");
            }

            Console.WriteLine("Pressione qualquer tecla para voltar ao menu");
            Console.ReadKey();
            Console.Clear();
        }

        public void UpdateStudents()
        {
            //implementar
        }

        public void SoftDelete()
        {
            Console.WriteLine("EXCLUSÃO DE ALUNO");
            string minhaConnectionString = DatabaseConnection.GetConnectionString("Default");

            while (true)
            {
                Console.WriteLine("Digite o ID do aluno que você deseja excluir (ou 0 para cancelar):");

                if (int.TryParse(Console.ReadLine(), out int idEscolhido))
                {
                    if (idEscolhido == 0)
                    {
                        Console.WriteLine("Exclusão de aluno cancelada.");
                        break;
                    }

                    _alunoService.SoftDelete(minhaConnectionString, idEscolhido);

                    Console.WriteLine($"Aluno com ID {idEscolhido} foi marcado como inativo com sucesso!");
                    break;
                }
                else
                {
                    Console.WriteLine("ID inválido. Por favor, insira um número válido.");
                }
            }
            Console.Clear();
        }

        //Métodos Extras: 
        /*private string ObterEntradaValida(string prompt, string mensagemErro)
        {
            Console.WriteLine(prompt);
            string entrada = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(entrada) || entrada.Length < 2 || entrada.Length > 100)
            {
                Console.WriteLine(mensagemErro);
                return null;
            }

            return entrada;
        }

        private DateTime ObterDataValida(string prompt)
        {
            Console.WriteLine(prompt);
            DateTime data;
            if (DateTime.TryParse(Console.ReadLine(), out data))
            {
                return data;
            }
            else
            {
                Console.WriteLine("Data de nascimento inválida! Tente novamente.");
                return DateTime.MinValue;  // Retorna uma data inválida para que o código saiba que houve erro.
            }
        }

        private char ObterSexoValido(string prompt)
        {
            Console.WriteLine(prompt);
            string sexo;
            do
            {
                sexo = Console.ReadLine().ToUpper();
                if (sexo != "M" && sexo != "F")
                {
                    Console.WriteLine("Entrada inválida. Digite Novamente!");
                }
            } while (sexo != "M" && sexo != "F");

            return char.Parse(sexo);
        }

        private string ObterEmailValido(string prompt, string mensagemErro)
        {
            Console.WriteLine(prompt);
            string email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email) || !new EmailAddressAttribute().IsValid(email) || !email.EndsWith("@exemplo.com"))
            {
                Console.WriteLine(mensagemErro);
                return null;
            }
            return email;
        }

        private async Task<Aluno> ObterEnderecoPorCepAsync(Aluno aluno)
        {
            Console.WriteLine("Digite o CEP:");
            var viaCepService = new ViaCepService(new HttpClient());
            string cep = Console.ReadLine();
            var endereco = await viaCepService.BuscarEnderecoPorCepAsync(cep);

            if (endereco != null)
            {
                aluno.Cep = endereco.Cep;
                aluno.Logradouro = endereco.Logradouro;
                aluno.Complemento = string.IsNullOrWhiteSpace(endereco.Complemento) ? "N/A" : endereco.Complemento;
                aluno.Bairro = endereco.Bairro;
                aluno.Localidade = endereco.Localidade;
                aluno.UF = endereco.UF;

                Console.WriteLine("Dados do Endereço do Aluno:");
                Console.WriteLine($"CEP: {endereco.Cep}");
                Console.WriteLine($"Logradouro: {endereco.Logradouro}");
                Console.WriteLine($"Complemento: {endereco.Complemento}");
                Console.WriteLine($"Bairro: {endereco.Bairro}");
                Console.WriteLine($"Localidade: {endereco.Localidade}");
                Console.WriteLine($"UF: {endereco.UF}");
            }
            else
            {
                Console.WriteLine("Endereço não encontrado.");
            }

            return aluno;
        }

        private bool ValidarAluno(Aluno aluno)
        {
            var validationContext = new ValidationContext(aluno);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(aluno, validationContext, validationResults, true);

            if (!isValid)
            {
                foreach (var validationResult in validationResults)
                {
                    Console.WriteLine($"Erro de validação: {validationResult.ErrorMessage}");
                }
            }

            return isValid;
        }*/
    }
}
