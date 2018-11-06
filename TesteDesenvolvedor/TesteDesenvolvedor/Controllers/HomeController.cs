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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GerenciarAlunos()
        {
            return View(AlunoDAO.BuscarAlunos());
        }

        public ActionResult CadastrarAluno()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarAluno(Aluno aluno)
        {
            if (aluno.Nome_Aluno != null)
            {
                AlunoDAO.SalvarAluno(aluno);
            }
            return RedirectToAction("GerenciarAlunos", "Home");
        }
    }
}