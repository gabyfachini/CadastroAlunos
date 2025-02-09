using CadastroAlunos.DAL;
using CadastroAlunos.Interfaces;
using CadastroAlunos.Services;
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
        private readonly IAlunoRepository _alunoRepository;

        public AlunoPresentation(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        public async Task RegisterStudents()
        {
            var novoAluno = new Aluno();

            novoAluno.Nome = ObterEntradaValida("Digite o nome do aluno:", "Nome deve ter entre 2 e 100 caracteres e não pode ser vazio.");
            novoAluno.Sobrenome = ObterEntradaValida("Digite o sobrenome do aluno:", "Sobrenome deve ter entre 2 e 100 caracteres e não pode ser vazio.");
            novoAluno.Nascimento = ObterDataValida("Digite a data de nascimento (formato: YYYY-MM-DD):");
            novoAluno.Sexo = ObterSexoValido("Digite o sexo (M/F):");
            novoAluno.Email = ObterEmailValido("Digite o email:", "E-mail inválido ou o e-mail não pertence ao domínio 'exemplo.com'.");
            novoAluno.Telefone = ObterEntradaValida("Digite o telefone:", "Telefone não pode ser vazio."); //ele aceita qualquer telefone, mesmo sendo vazio

            novoAluno.DataDeAtualizacao = novoAluno.DataDeCadastro = DateTime.Now;
            novoAluno.Ativo = true;

            novoAluno = await ObterEnderecoPorCepAsync(novoAluno); //se eu colocar um cep ruim,ele zera tudo e não da mensagem de erro
            /*Thread.Sleep(2000);*/

            if (ValidarAluno(novoAluno))
             {
                 await _alunoRepository.CadastrarAlunoAsync(novoAluno);

                 Console.WriteLine("Aluno cadastrado com sucesso!");
             }

            Console.Clear();
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

        private DateTime ObterDataValida(string prompt) //aqui ele ta aceitando datas de nascimento invalidas como 20-32-34
        {
            DateTime data;
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

        private string ObterEmailValido(string prompt, string mensagemErro) //aqui ele ta aceitando e-mails só com @exemplo.com
        {
            string email;
            do
            {
                Console.WriteLine(prompt);
                email = Console.ReadLine().Trim();

                if (!new EmailAddressAttribute().IsValid(email) || !email.EndsWith("@exemplo.com"))
                {
                    Console.WriteLine(mensagemErro);
                }
                else
                {
                    return email;
                }

            } while (true);
        }

        private async Task<Aluno> ObterEnderecoPorCepAsync(Aluno aluno)
        {
            var viaCepService = new ViaCepService(new HttpClient());
            string cep;
            Endereco endereco;

            do
            {
                Console.WriteLine("Digite o CEP:");
                cep = Console.ReadLine()?.Trim();

                endereco = await viaCepService.BuscarEnderecoPorCepAsync(cep);

                if (endereco == null)
                {
                    Console.WriteLine("Endereço não encontrado. Tente novamente.");
                }

            } while (endereco == null);

            // Preenchendo os dados do aluno com o endereço encontrado
            aluno.Cep = endereco.Cep;
            aluno.Logradouro = endereco.Logradouro;
            aluno.Complemento = string.IsNullOrWhiteSpace(endereco.Complemento) ? "N/A" : endereco.Complemento;
            aluno.Bairro = endereco.Bairro;
            aluno.Localidade = endereco.Localidade;
            aluno.UF = endereco.UF;

            // Exibindo os dados do endereço antes de continuar
            Console.WriteLine("\nDados do Endereço do Aluno:");
            Console.WriteLine($"CEP: {endereco.Cep}");
            Console.WriteLine($"Logradouro: {endereco.Logradouro}");
            Console.WriteLine($"Complemento: {endereco.Complemento}");
            Console.WriteLine($"Bairro: {endereco.Bairro}");
            Console.WriteLine($"Localidade: {endereco.Localidade}");
            Console.WriteLine($"UF: {endereco.UF}");

            // Aguarda o usuário pressionar uma tecla antes de continuar
            Console.WriteLine("\nPressione Enter para continuar...");
            Console.ReadLine();

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
        }
    }
}
