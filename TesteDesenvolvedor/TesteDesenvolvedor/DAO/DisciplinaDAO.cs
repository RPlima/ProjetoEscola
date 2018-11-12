using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesteDesenvolvedor.Models;

namespace TesteDesenvolvedor.DAO
{
    public class DisciplinaDAO
    {

        #region Buscar Todas as Disciplinas
        public static List<Disciplina> BuscarDisciplinas()
        {
            return DbContext.SelectDisciplinas();
        }
        #endregion

        #region Buscar Disciplina Por Id
        public static Disciplina BuscarDisciplinaById(int id)
        {
            return DbContext.SelectDisciplinaById(id);
        }
        #endregion

        #region Cadastrar Disciplina
        public static void CadastrarDisciplina(Disciplina disciplina)
        {
            DbContext.InsertDisciplinaDb(disciplina);
        }
        #endregion

        #region Editar a Disciplina
        public static void EditarDisciplina(Disciplina disciplina)
        {
            DbContext.UpadadeDisciplinaDb(disciplina);
        }
        #endregion

        #region Deletar Disciplina
        public static void DeletarDisciplina(int id)
        {
            DbContext.DeleteDisciplinaDb(id);
        }
        #endregion

    }
}