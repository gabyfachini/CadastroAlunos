using CadastroAlunos.Interfaces;
using CadastroAlunos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;  
using Dapper;

namespace CadastroAlunos.Services
{
    public class AlunoService : IAlunoService //responsável por realizar operações como CadastrarAluno e outras manipulações de dados.
    {
        private readonly IAlunoRepository _alunoRepository;

        public AlunoService(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        public List<Aluno> ListarAlunos()
        {
            return _alunoRepository.GetList();
        }
        public void SoftDelete(string connectionString, int alunoId)
        {
            _alunoRepository.SoftDelete(alunoId);

        }
    }
}
