using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TesteDesenvolvedor.Models;

namespace TesteDesenvolvedor.DAO
{
    public class DbContext
    {
        /*  string strcon = "Data Source=10.5.0.4\\PRD;Initial Catalog=SINE_PRD;User ID=SINE_app;Password=s1n3pr0d@2014;";
                SqlDataReader dr = null;
                SqlConnection conn = new SqlConnection(strcon);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = ExecutaQueryParaLiparCampo;

   
         *    List<SqlParameter> param = new List<SqlParameter>();
               param.Add(new SqlParameter("@Idf_Vaga", SqlDbType.Int, 4));
                   param.Add(new SqlParameter("@Des_Vaga", SqlDbType.VarChar));
                   param.Add(new SqlParameter("@Nme_Sinonimo", SqlDbType.VarChar, 50));

                   param[0].Value = this.Idf_Vaga;
                   param[1].Value = this.descricao;
                   param[2].Value = this.funcao;

                   string strcon = "Data Source=10.5.0.4\\PRD;Initial Catalog=SINE_PRD;User ID=SINE_app;Password=s1n3pr0d@2014;";
               SqlDataReader dr = null;
               SqlConnection conn = new SqlConnection(strcon);
               SqlCommand cmd = new SqlCommand();
               cmd.Connection = conn;



                            cmd.CommandType = CommandType.Text;
                           cmd.CommandText = ExecutaQueryInsert;
                           cmd.Transaction = trans;



                           cmd.ExecuteNonQuery();
                           cmd.Parameters.Clear();

                           trans.Commit();
                           conn.Close();

                    trans.Rollback();
                    conn.Close();
       */

        #region Acesso Ao Banco
        private const string strcon = "Data Source=CQI-DEV-0833;Initial Catalog=Escola;Integrated Security=SSPI;";

    
        public static SqlConnection AcessarDb(SqlCommand cmd, SqlConnection conn , string Query)
        {
            conn = new SqlConnection(strcon);
            cmd.Connection = conn;

            if(!conn.State.Equals(true))
            conn.Open();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = Query;
            return conn;
        }
        #endregion

        #region Query's de Acesso ao Banco
        private const string InsertAluno = @"insert into Aluno(Nome) values(@Nome)";

        private const string BuscarTodosAlunos = @"Select * From Aluno";

        private const string BuscarAlunoId = @"Select * From Aluno where IdAluno = @IdAluno";

        private const string UpdateAluno = @"Update Aluno set Nome = @Nome where IdAluno = @IdAluno";

        private const string DeleteAluno = @"Delete Aluno where IdAluno = @IdAluno";
        #endregion

        #region Insert de Alunos
        public static void InsertAlunoDb(Aluno aluno)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = null;
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Nome",SqlDbType.VarChar));
            param[0].Value = aluno.Nome_Aluno;

            cmd.Parameters.Add(param[0]);
            conn = AcessarDb(cmd, conn, InsertAluno);
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                cmd.Transaction = trans;

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                trans.Commit();
                conn.Close();
            }
        }
        #endregion

        #region Processo de Select de Alunos
        public static List<Aluno> SelectAlunos()
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr = null;
            SqlConnection conn = null;
            AcessarDb(cmd,conn , BuscarTodosAlunos);
            dr = cmd.ExecuteReader();

            Aluno aluno;
            List<Aluno> alunos = new List<Aluno>();
            while (dr.Read())
            {
                aluno = new Aluno();

                if (dr["Nome"] != DBNull.Value)
                    aluno.IdAluno = Convert.ToInt32(dr["IdAluno"]);
                    aluno.Nome_Aluno = Convert.ToString(dr["Nome"]);
                if(aluno.Nome_Aluno != null)
                {
                    alunos.Add(aluno);
                }
            }
           
            return alunos;
        }
        #endregion

        #region Select aluno Por id
        public static Aluno BuscarAlunoById(int id)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr = null;
            SqlConnection conn = null;
            AcessarDb(cmd, conn, BuscarAlunoId);

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@IdAluno", SqlDbType.Int));
            param[0].Value = id;

            cmd.Parameters.Add(param[0]);

            dr = cmd.ExecuteReader();

            Aluno aluno = new Aluno();
            while (dr.Read())
            {
                if (dr["Nome"] != DBNull.Value)
                    aluno.IdAluno = Convert.ToInt32(dr["IdAluno"]);
                    aluno.Nome_Aluno = Convert.ToString(dr["Nome"]);
            }

            cmd.Parameters.Clear();
            return aluno;
        }
        #endregion

        #region Update de Aluno
        public static void UpdateAlunoDb(Aluno aluno)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = null;
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Nome", SqlDbType.VarChar));
            param.Add(new SqlParameter("@IdAluno", SqlDbType.Int));
            param[0].Value = aluno.Nome_Aluno;
            param[1].Value = aluno.IdAluno;

            cmd.Parameters.Add(param[0]);
            cmd.Parameters.Add(param[1]);

            conn = AcessarDb(cmd, conn, UpdateAluno);
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                cmd.Transaction = trans;

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                trans.Commit();
                conn.Close();
            }
        }

        #endregion

        #region Delete de Aluno
        public static void DeleteAlunoDb(int id)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = null;
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@IdAluno", SqlDbType.Int));
            param[0].Value = id;

            cmd.Parameters.Add(param[0]);

            conn = AcessarDb(cmd, conn, DeleteAluno);
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                cmd.Transaction = trans;

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                trans.Commit();
                conn.Close();
            }
        }
        #endregion

    }
}