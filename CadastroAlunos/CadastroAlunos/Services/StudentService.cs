using CadastroAlunos.Interfaces;
using CadastroAlunos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroAlunos.Services
{
    public class StudentService : IAlunoService //responsável por realizar operações como CadastrarAluno e outras manipulações de dados.
    {
        private readonly IAlunoRepository _alunoRepository;

        public StudentService(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        public List<Student> ListarAlunos()
        {
            return _alunoRepository.GetList();
        }
    }
}
