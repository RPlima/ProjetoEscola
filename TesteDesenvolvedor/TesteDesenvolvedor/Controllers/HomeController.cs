using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesteDesenvolvedor.DAO;
using TesteDesenvolvedor.Models;

namespace TesteDesenvolvedor.Controllers
{
    public class HomeController : Controller
    {
        #region home page
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region Pag Gerenciar Alunos
        public ActionResult GerenciarAlunos()
        {
            return View(AlunoDAO.BuscarAlunos());
        }
        #endregion

        #region Pág CadastrarAluno
        public ActionResult CadastrarAluno()
        {
            return View();
        }
        #endregion

        #region Cadastro do Aluno
        [HttpPost]
        public ActionResult CadastrarAluno(Aluno aluno)
        {
            if (aluno.Nome_Aluno != null)
            {
                AlunoDAO.SalvarAluno(aluno);
            }
            return RedirectToAction("GerenciarAlunos", "Home");
        }
        #endregion

        #region Pág Editar Aluno
        public ActionResult EditarAluno(int idAluno)
        {
            return View(AlunoDAO.BuscarAlunoById(idAluno));
        }
        #endregion

        #region Edição do Aluno
        [HttpPost]
        public ActionResult EditarAluno(Aluno alunoNovo)
        {
            if(alunoNovo.IdAluno == 0 || alunoNovo.Nome_Aluno == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Aluno alunoOriginal = AlunoDAO.BuscarAlunoById(alunoNovo.IdAluno);
            alunoOriginal.Nome_Aluno = alunoNovo.Nome_Aluno;
            AlunoDAO.EditarAluno(alunoOriginal);

            return RedirectToAction("GerenciarAlunos", "Home");
      
        }
        #endregion

        public ActionResult ExcluirAluno(int IdAluno)
        {
            AlunoDAO.ExcluirAluno(IdAluno);
            return RedirectToAction("GerenciarAlunos", "Home");
        }
    }
}