using CadastroAlunos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroAlunos.Models
{
    public class StudentPresentation
    {
        private readonly IAlunoService _alunoService;

        public StudentPresentation(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        public void BuscarAluno()
        {
            Console.Clear();
            Console.WriteLine("REGISTRO DE ALUNOS\n");

            List<Student> alunos = _alunoService.ListarAlunos();

            foreach (var aluno in alunos)
            {
                Console.WriteLine($"ID: {aluno.Id}\nNome: {aluno.Nome} {aluno.Sobrenome}\nNascimento: {aluno.Nascimento}\nSexo: {aluno.Sexo}\nEmail: {aluno.Email}\nTelefone: {aluno.Telefone}\nEndereço: {aluno.Logradouro}, {aluno.Complemento}.{aluno.Bairro}, {aluno.Localidade} - {aluno.UF} (CEP: {aluno.Cep})\n");

            }

            Console.WriteLine("Pressione qualquer tecla para voltar ao menu");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
