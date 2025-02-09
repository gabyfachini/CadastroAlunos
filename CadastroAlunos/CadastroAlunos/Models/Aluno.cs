using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroAlunos.Models
{
    public class Aluno
    {
        public int Id { get; set; } //Obrigatório no Banco de Dados

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Nome deve ter entre 2 e 100 caracteres")]
        public string Nome { get; set; } //Obrigatório no Banco de Dados

        [Required(ErrorMessage = "O sobrenome é obrigatório")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Sobrenome deve ter entre 2 e 100 caracteres")]
        public string Sobrenome { get; set; } //Obrigatório no Banco de Dados

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        [DataType(DataType.Date, ErrorMessage = "Data de nascimento inválida")]
        public DateTime Nascimento { get; set; } //Obrigatório no Banco de Dados

        [Required(ErrorMessage = "O sexo é obrigatório")]
        [RegularExpression("^[MF]$", ErrorMessage = "Sexo deve ser 'M' para Masculino ou 'F' para Feminino")]
        public char Sexo { get; set; } //Obrigatório no Banco de Dados

        [Required(ErrorMessage = "E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }
        public DateTime DataDeCadastro { get; set; } //Obrigatório no Banco de Dados
        public string Telefone { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
        public DateTime? DataDeAtualizacao { get; set; } //CAMPO DE AUDITORIA - Data da última atualização (pode ser nula) 
        public bool Ativo { get; set; } //CAMPO DE AUDITORIA | 1 = ativo e 0 = excluído | Obrigatório no Banco de Dados

    }
}
