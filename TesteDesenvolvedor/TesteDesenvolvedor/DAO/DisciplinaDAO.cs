using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesteDesenvolvedor.Models;

namespace TesteDesenvolvedor.DAO
{
    public class DisciplinaDAO
    {
        public static List<Disciplina> BuscarDisciplinas()
        {
            return DbContext.SelectDisciplinas();
        }

        public static void CadastrarDisciplina(Disciplina disciplina)
        {
            DbContext.InsertDisciplinaDb(disciplina);
        }
    }
}