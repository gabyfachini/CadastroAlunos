using CadastroAlunos.DAL;
using CadastroAlunos.Interfaces;
using CadastroAlunos.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CadastroAlunos.Models
{
    public class AlunoPresentation

    {
        private readonly IAlunoService _alunoService;
        private readonly IViaCepService _viaCepService;
        private readonly IAlunoRepository _alunoRepository;

        public AlunoPresentation(IAlunoService alunoService, IViaCepService viaCepService, IAlunoRepository alunoRepository)
        {
            _alunoService = alunoService;
            _viaCepService = viaCepService;
            _alunoRepository = alunoRepository;
        }

        public async Task RegisterStudents()
        {
            var novoAluno = new Aluno(); //não é interesante usar isso 

            novoAluno.Nome = ObterEntradaValida("Digite o nome do aluno:", "Nome deve ter entre 2 e 100 caracteres e não pode ser vazio.");
            novoAluno.Sobrenome = ObterEntradaValida("Digite o sobrenome do aluno:", "Sobrenome deve ter entre 2 e 100 caracteres e não pode ser vazio.");
            novoAluno.Nascimento = ObterDataNascimento("Digite a data de nascimento | Formato DD/MM/AAAA):");
            novoAluno.Sexo = ObterSexoValido("Digite o sexo (M/F):");
            novoAluno.Email = ObterEmailValido("Digite o email:", "E-mail inválido!");
            novoAluno.Telefone = ObterTelefoneValido("Digite o telefone | Formato (XX) 9XXXX-XXXX:", "Telefone incorreto!");

            novoAluno.DataDeAtualizacao = novoAluno.DataDeCadastro = DateTime.Now;
            novoAluno.Ativo = true;

            Console.WriteLine("Digite o CEP:");
            novoAluno.Cep = Console.ReadLine()?.Trim();

            try
            {
                var endereco = await ObterEnderecoPorCepAsync(novoAluno.Cep);

                if (endereco == null)
                {
                    Console.WriteLine("Cadastro não concluído devido a erro no endereço.");
                    return;
                }

                // Atribuir os dados do endereço ao aluno
                novoAluno.Logradouro = endereco.Logradouro;
                novoAluno.Complemento = string.IsNullOrWhiteSpace(endereco.Complemento) ? "N/A" : endereco.Complemento;
                novoAluno.Bairro = endereco.Bairro;
                novoAluno.Localidade = endereco.Localidade;
                novoAluno.UF = endereco.UF;

                if (ValidarAluno(novoAluno))
                {
                    await _alunoRepository.CadastrarAlunoAsync(novoAluno);
                    Console.WriteLine("Aluno cadastrado com sucesso!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar endereço: {ex.Message}");
            }
            Thread.Sleep(5000); //aqui ta aparecendo o menu antes dos dados
            /*Console.Clear();*/
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
        private string ObterEntradaValida(string prompt, string mensagemErro)
        {
            string entrada;

            do
            {
                Console.WriteLine(prompt);
                entrada = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(entrada) || entrada.Length < 2 || entrada.Length > 100)
                {
                    Console.WriteLine(mensagemErro);
                }
                else
                {
                    return entrada;
                }

            } while (true);
        }

        private DateTime ObterDataNascimento(string prompt) 
        {
            DateTime data;
            string[] formatos = {"dd/MM/yyyy"};
            do
            {
                Console.WriteLine(prompt);
                string entrada = Console.ReadLine();

                if (!DateTime.TryParse(entrada, out data))
                {
                    Console.WriteLine("Data de nascimento inválida! Tente novamente.");
                }
                else
                {
                    return data;
                }

            } while (true);
        }

        private char ObterSexoValido(string prompt)
        {
            string sexo;
            do
            {
                Console.WriteLine(prompt);
                sexo = Console.ReadLine().Trim().ToUpper();

                if (sexo != "M" && sexo != "F")
                {
                    Console.WriteLine("Entrada inválida. Digite 'M' para masculino ou 'F' para feminino.");
                }

            } while (sexo != "M" && sexo != "F");

            return char.Parse(sexo);
        }

        private string ObterEmailValido(string prompt, string mensagemErro) 
        {
            string email;
            string padraoEmail = @"^[^@\s]+@[^@\s]+\.(com|com\.br|br)$"; // Regex para validar o e-mail

            do
            {
                Console.WriteLine(prompt);
                email = Console.ReadLine().Trim();

                if (!Regex.IsMatch(email, padraoEmail, RegexOptions.IgnoreCase))
                {
                    Console.WriteLine(mensagemErro);
                }
                else
                {
                    return email;
                }

            } while (true);
        }
        private string ObterTelefoneValido(string prompt, string mensagemErro)
        {
            string telefone;
            string padraoTelefone = @"^\(\d{2}\) 9\d{4}-\d{4}$"; // Regex para telefone BR

            do
            {
                Console.WriteLine(prompt);
                telefone = Console.ReadLine().Trim();

                if (!Regex.IsMatch(telefone, padraoTelefone))
                {
                    Console.WriteLine(mensagemErro);
                }
                else
                {
                    return telefone;
                }

            } while (true);
        }
        public async Task<Endereco> ObterEnderecoPorCepAsync(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
            {
                throw new ArgumentException("CEP inválido.");
            }

            var endereco = await _viaCepService.BuscarEnderecoPorCepAsync(cep);

            if (endereco == null)
            {
                throw new Exception("Endereço não encontrado.");
            }

            return endereco;
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
        }
    }
}
