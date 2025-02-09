using CadastroAlunos.Interfaces;
using CadastroAlunos.Models;
using CadastroAlunos.Services;
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
        private readonly IViaCepService _viaCepService; 

        public AlunoService(IAlunoRepository alunoRepository, IViaCepService? viaCepService = null)
        {
            _alunoRepository = alunoRepository;
            _viaCepService = viaCepService; 
        }

        public async Task<Endereco> ObterEnderecoPorCepAsync(string cep, Aluno aluno)
        {
            // Validação do CEP
            if (string.IsNullOrWhiteSpace(cep))
            {
                throw new ArgumentException("CEP inválido.");
            }

            // Busca o endereço utilizando o serviço ViaCepService (ou outro serviço)
            var endereco = await _viaCepService.BuscarEnderecoPorCepAsync(cep);

            // Verifica se o endereço foi encontrado
            if (endereco == null)
            {
                throw new Exception("Endereço não encontrado.");
            }

            // Se o endereço for válido, realiza o cadastro do aluno
            await _alunoRepository.CadastrarAlunoAsync(aluno);

            // Retorna o endereço após o cadastro
            return endereco;
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
