﻿using CadastroAlunos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroAlunos.Interfaces
{
    public interface IAlunoService
    {
        List<Aluno> ListarAlunos(); //Vale para o case 2 e 3
        void SoftDelete(string connectionString, int alunoId);

    }
}
