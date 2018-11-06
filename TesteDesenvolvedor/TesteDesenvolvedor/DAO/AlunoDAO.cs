using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesteDesenvolvedor.Models;

namespace TesteDesenvolvedor.DAO
{
    public class AlunoDAO
    {
        public static List<Aluno> BuscarAlunos()
        {
            return DbContext.SelectAlunos();
        }

        public static void SalvarAluno(Aluno aluno)
        {
               DbContext.InsertAlunoDb(aluno);
        }
    }
}