using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TesteDesenvolvedor.Models;

namespace TesteDesenvolvedor.DAO
{
    public class AlunoDAO
    {

        #region Listar Todos os Alunos
        public static List<Aluno> BuscarAlunos()
        {
            return DbContext.SelectAlunos();
        }
        #endregion

        #region Salvar Aluno
        public static void SalvarAluno(Aluno aluno)
        {
               DbContext.InsertAlunoDb(aluno);
        }
        #endregion

        #region Buscar Aluno Por id
        public static Aluno BuscarAlunoById(int id)
        {
            return DbContext.BuscarAlunoById(id);
        }
        #endregion

        #region Editar Aluno
        public static void EditarAluno(Aluno aluno)
        {
            DbContext.UpdateAlunoDb(aluno);
        }
        #endregion

        #region Excluir Aluno
        public static void ExcluirAluno(int id)
        {
            DbContext.DeleteAlunoDb(id);
        }
        #endregion

    }
}