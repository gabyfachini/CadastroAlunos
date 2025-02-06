using CadastroAlunos.Interfaces;
using CadastroAlunos.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CadastroAlunos.DAL
{
    public class AlunoDAL : IAlunoRepository
    {
        private string _connectionString;
        public AlunoDAL(IConfiguration iconfiguration)
        {
            _connectionString = iconfiguration.GetConnectionString("Default");
        }
        public List<Student> GetList()
        {
            var listAluno = new List<Student>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    // Consulta SQL com o filtro Ativo = 1, ou seja, não exibe usuários que estão inativados no Banco de Dados
                    SqlCommand cmd = new SqlCommand("SELECT Id, Nome, Sobrenome, Nascimento, Sexo, Email, Telefone, Cep, Logradouro, Complemento, Bairro, Localidade, UF, DataDeAtualizacao, Ativo FROM Aluno WHERE Ativo = 1", con);
                    cmd.CommandType = CommandType.Text; // Mudando de CommandType.StoredProcedure para CommandType.Text para usar a consulta SQL diretamente
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        listAluno.Add(new Student
                        {
                            Id = Convert.ToInt32(rdr[0]),
                            Nome = Convert.ToString(rdr[1]),
                            Sobrenome = Convert.ToString(rdr[2]),
                            Nascimento = Convert.ToDateTime(rdr[3]),
                            Sexo = Convert.ToChar(rdr[4]),
                            Email = Convert.ToString(rdr[5]),
                            Telefone = Convert.ToString(rdr[6]),
                            Cep = Convert.ToString(rdr[7]),
                            Logradouro = Convert.ToString(rdr[8]),
                            Complemento = Convert.ToString(rdr[9]),
                            Bairro = Convert.ToString(rdr[10]),
                            Localidade = Convert.ToString(rdr[11]),
                            UF = Convert.ToString(rdr[12]),
                            DataDeAtualizacao = Convert.ToDateTime(rdr[13]),
                            Ativo = Convert.ToBoolean(rdr[14])
                            //Observação: aqui eu não trouxe a DataDeCadastro porque é uma informação interna do Banco de Dados, caso seja necessária, é precisso incluir aqui para visualização da informação
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listAluno;
        }
    }
}
