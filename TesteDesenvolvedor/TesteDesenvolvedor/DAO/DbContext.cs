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
        

        #region Acesso Ao Banco
        private const string strcon = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Escola;Integrated Security=SSPI;";


        private static SqlConnection AcessarDb(SqlCommand cmd, SqlConnection conn, string Query)
        {
            conn = new SqlConnection(strcon);
            cmd.Connection = conn;

            if (!conn.State.Equals(true))
                conn.Open();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = Query;
            return conn;
        }
        #endregion

        #region Query's de Acesso ao Banco

        #region Alunos Query's
        private const string InsertAluno = @"insert into Aluno(Nome_Aluno) values(@Nome_Aluno)";

        private const string BuscarTodosAlunos = @"Select * From Aluno";

        private const string BuscarAlunoId = @"Select * From Aluno where IdAluno = @IdAluno";

        private const string UpdateAluno = @"Update Aluno set Nome_Aluno = @Nome_Aluno where IdAluno = @IdAluno";

        private const string DeleteAluno = @"Delete Aluno where IdAluno = @IdAluno";
        #endregion

        #region Disciplinas Query
        private const string BuscarTodasDisciplinas = @"Select IdDisciplina,NomeDisciplina,Nome_Aluno, d.IdAluno 
                                                        from Disciplina d Join Aluno a on d.IdAluno = a.IdAluno";
        private const string BuscarDiciplinaById = @"Select * from Disciplina where IdDisciplina = @IdDisciplina";
        private const string InsertDisciplina = @"insert into Disciplina(NomeDisciplina, IdAluno) values(@NomeDisciplina, @IdAluno)";
        private const string UpdateDisciplina = @"Update Disciplina set NomeDisciplina = @NomeDisciplina, IdAluno = @IdAluno where IdDisciplina = @IdDisciplina";
        private const string DeleteDisciplina = @"Delete Disciplina where IdDisciplina = @IdDisciplina";
        private const string SelectDisciplinaByAluno = @"Select * From Disciplina d join Aluno a on d.IdAluno = a.IdAluno where d.IdAluno = @IdAluno";
        #endregion

        #endregion

        #region CRUD da tabela Aluno

        #region Insert de Alunos
        public static void InsertAlunoDb(Aluno aluno)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = null;
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@Nome_Aluno", SqlDbType.VarChar));
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
            AcessarDb(cmd, conn, BuscarTodosAlunos);
            dr = cmd.ExecuteReader();

            Aluno aluno;
            List<Aluno> alunos = new List<Aluno>();
            while (dr.Read())
            {
                aluno = new Aluno();

                if (dr["Nome_Aluno"] != DBNull.Value)
                    aluno.IdAluno = Convert.ToInt32(dr["IdAluno"]);
                aluno.Nome_Aluno = Convert.ToString(dr["Nome_Aluno"]);
                if (aluno.Nome_Aluno != null)
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
                if (dr["Nome_Aluno"] != DBNull.Value)
                    aluno.IdAluno = Convert.ToInt32(dr["IdAluno"]);
                aluno.Nome_Aluno = Convert.ToString(dr["Nome_Aluno"]);
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
            param.Add(new SqlParameter("@Nome_Aluno", SqlDbType.VarChar));
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
            //Deletando todas as Disciplinas Referente ao Aluno, antes de deletar o Aluno. Desse modo, não haverá conflito.
            DeleteDisciplinaDb(id);

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

        #region Select Disciplinas por id Aluno
        public static List<Disciplina> SelectDisciplinaByAlunoDb(int id)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr = null;
            SqlConnection conn = null;
            AcessarDb(cmd, conn, SelectDisciplinaByAluno);
            List<SqlParameter> param = new List<SqlParameter>();

            param.Add(new SqlParameter("@IdAluno", SqlDbType.Int));
            param[0].Value = id;

            cmd.Parameters.Add(param[0]);


            dr = cmd.ExecuteReader();

            Disciplina disciplina;
            Aluno aluno = new Aluno();
            List<Disciplina> disciplinas = new List<Disciplina>();
            while (dr.Read())
            {
                disciplina = new Disciplina();
                if (dr["NomeDisciplina"] != DBNull.Value)
                    disciplina.IdAluno = Convert.ToInt32(dr["IdAluno"]);
                disciplina.IdDisciplina = Convert.ToInt32(dr["IdDisciplina"]);

                disciplina.Aluno = aluno;

                disciplina.Aluno.Nome_Aluno = Convert.ToString(dr["Nome_Aluno"]);
                disciplina.Aluno.IdAluno = Convert.ToInt32(dr["IdAluno"]);

                disciplina.NomeDisciplina = Convert.ToString(dr["NomeDisciplina"]);
                if (disciplina.NomeDisciplina != null)
                {
                    disciplinas.Add(disciplina);
                }
            }

            return disciplinas;
        }

        #endregion

        #endregion

        #region CRUD da Tabela Disciplina

        #region Processo de Select de Disciplinas
        public static List<Disciplina> SelectDisciplinas()
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr = null;
            SqlConnection conn = null;
            AcessarDb(cmd, conn, BuscarTodasDisciplinas);
            dr = cmd.ExecuteReader();

            Disciplina disciplina;
            Aluno aluno = new Aluno();
            List<Disciplina> disciplinas = new List<Disciplina>();
            while (dr.Read())
            {
                disciplina = new Disciplina();
                if (dr["NomeDisciplina"] != DBNull.Value)
                disciplina.IdAluno = Convert.ToInt32(dr["IdAluno"]);
                disciplina.IdDisciplina = Convert.ToInt32(dr["IdDisciplina"]);
                disciplina.Aluno = aluno;

                disciplina.Aluno.Nome_Aluno = Convert.ToString(dr["Nome_Aluno"]);


                disciplina.NomeDisciplina = Convert.ToString(dr["NomeDisciplina"]);
                if (disciplina.NomeDisciplina != null)
                {
                    disciplinas.Add(disciplina);
                }
            }

            return disciplinas;
        }
        #endregion

        #region Insert de Disciplina
        public static void InsertDisciplinaDb(Disciplina disciplina)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = null;
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@NomeDisciplina", SqlDbType.VarChar));
            param.Add(new SqlParameter("@IdAluno", SqlDbType.VarChar));
            param[0].Value = disciplina.NomeDisciplina;
            param[1].Value = disciplina.IdAluno;

            cmd.Parameters.Add(param[0]);
            cmd.Parameters.Add(param[1]);
            conn = AcessarDb(cmd, conn, InsertDisciplina);
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

        #region Update de Disciplina
        public static void UpadadeDisciplinaDb(Disciplina disciplina)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = null;
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@NomeDisciplina", SqlDbType.VarChar));
            param.Add(new SqlParameter("@IdAluno", SqlDbType.Int));
            param.Add(new SqlParameter("@IdDisciplina", SqlDbType.Int));
            param[0].Value = disciplina.NomeDisciplina;
            param[1].Value = disciplina.IdAluno;
            param[2].Value = disciplina.IdDisciplina;

            cmd.Parameters.Add(param[0]);
            cmd.Parameters.Add(param[1]);
            cmd.Parameters.Add(param[2]);

            conn = AcessarDb(cmd, conn, UpdateDisciplina);
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

        #region Select de Disciplina por Id
        public static Disciplina SelectDisciplinaById(int id)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr = null;
            SqlConnection conn = null;
            AcessarDb(cmd, conn, BuscarDiciplinaById);

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@IdDisciplina", SqlDbType.Int));
            param[0].Value = id;

            cmd.Parameters.Add(param[0]);

            dr = cmd.ExecuteReader();

            Disciplina disciplina = new Disciplina();
            while (dr.Read())
            {
                if (dr["NomeDisciplina"] != DBNull.Value)
                    disciplina.IdAluno = Convert.ToInt32(dr["IdAluno"]);
                    disciplina.IdDisciplina = Convert.ToInt32(dr["IdDisciplina"]);
                    disciplina.NomeDisciplina = Convert.ToString(dr["NomeDisciplina"]);
            }

            cmd.Parameters.Clear();
            return disciplina;
        }
        #endregion

        #region Delete de Disciplina
        public static void DeleteDisciplinaDb(int id)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = null;
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@IdDisciplina", SqlDbType.Int));
            param[0].Value = id;

            cmd.Parameters.Add(param[0]);

            conn = AcessarDb(cmd, conn, DeleteDisciplina);
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

        #endregion

    }
}
