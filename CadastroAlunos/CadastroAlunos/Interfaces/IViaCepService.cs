using CadastroAlunos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroAlunos.Interfaces
{
    public interface IViaCepService
    {
        Task<Endereco?> BuscarEnderecoPorCepAsync(string cep);
    }
}
