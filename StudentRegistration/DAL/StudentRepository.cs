using StudentRegistration.Interfaces;
using StudentRegistration.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace StudentRegistration.DAL
{
    public class StudentRepository : IStudentRepository

    {
        private string _connectionString;
        public StudentRepository(IConfiguration iconfiguration)
        {
            _connectionString = iconfiguration.GetConnectionString("Default");
        }
        public async Task RegisterStudentAsync(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "Student cannot be null.");
            }

            var sql = @"
            INSERT INTO Aluno 
            (Nome, Sobrenome, Nascimento, Sexo, Email, Telefone, Cep, Logradouro, Complemento, Bairro, Localidade, UF, DataDeCadastro, DataDeAtualizacao, Ativo)
            VALUES
            (@Nome, @Sobrenome, @Nascimento, @Sexo, @Email, @Telefone, @Cep, @Logradouro, @Complemento, @Bairro, @Localidade, @UF,  @DataDeCadastro, @DataDeAtualizacao, @Ativo)";

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Nome", student.Nome);
                    command.Parameters.AddWithValue("@Sobrenome", student.Sobrenome);
                    command.Parameters.AddWithValue("@Nascimento", student.Nascimento);
                    command.Parameters.AddWithValue("@Sexo", student.Sexo);
                    command.Parameters.AddWithValue("@Email", student.Email);
                    command.Parameters.AddWithValue("@Telefone", student.Telefone);
                    command.Parameters.AddWithValue("@Cep", student.Cep);
                    command.Parameters.AddWithValue("@Logradouro", student.Logradouro);
                    command.Parameters.AddWithValue("@Complemento", student.Complemento);
                    command.Parameters.AddWithValue("@Bairro", student.Bairro);
                    command.Parameters.AddWithValue("@Localidade", student.Localidade);
                    command.Parameters.AddWithValue("@UF", student.UF);
                    command.Parameters.AddWithValue("@DataDeCadastro", student.RegistrationDate);
                    command.Parameters.AddWithValue("@DataDeAtualizacao", student.UpdateDate);
                    command.Parameters.AddWithValue("@Ativo", student.IsActive);

                    try
                    {
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();  
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error registering student: {ex.Message}");
                    }
                }
            }
        }
        public List<Student> GetList()
        {
            var studentList = new List<Student>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    // SQL query with the filter Active = 1, that is, it does not display users that are inactive in the Database
                    SqlCommand cmd = new SqlCommand("SELECT Id, Nome, Sobrenome, Nascimento, Sexo, Email, Telefone, Cep, Logradouro, Complemento, Bairro, Localidade, UF, DataDeAtualizacao, Ativo FROM Aluno WHERE Ativo = 1", con);
                    cmd.CommandType = CommandType.Text; // Changing from CommandType.StoredProcedure to CommandType.Text to use SQL query directly
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        studentList.Add(new Student
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
                            UpdateDate = Convert.ToDateTime(rdr[13]),
                            IsActive = Convert.ToBoolean(rdr[14])
                            // Note: I did not bring the RegistrationDate because it is internal information from the database. If needed, it can be added here.
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return studentList;
        }
        public async Task SoftDelete(int id)
        {
            var sql = @"
            UPDATE Aluno
            SET Ativo = 0, DataDeAtualizacao = @DataDeAtualizacao
            WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@DataDeAtualizacao", DateTime.Now);

                    try
                    {
                        await connection.OpenAsync();  
                        int rowsAffected = await command.ExecuteNonQueryAsync();  

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Student with ID {id} has been marked as inactive.");
                            Console.WriteLine("Enter the desired option: ");
                        }
                        else
                        {
                            Console.WriteLine("Student not found in the database.");
                            Console.WriteLine("Enter the desired option: ");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deactivating student: {ex.Message}");
                    }
                }
            }
        }
    }
}
