using CadastroAlunos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CadastroAlunos.Interfaces
{
    public interface IAlunoRepository
    {
        List<Aluno> GetList(); //Vale para o case 2 e 3
        Task SoftDelete(int id); //SoftDelete

    }
}
