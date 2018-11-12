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

        #region CRUD dos Alunos

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
            if (alunoNovo.IdAluno == 0 || alunoNovo.Nome_Aluno == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Aluno alunoOriginal = AlunoDAO.BuscarAlunoById(alunoNovo.IdAluno);
            alunoOriginal.Nome_Aluno = alunoNovo.Nome_Aluno;
            AlunoDAO.EditarAluno(alunoOriginal);

            return RedirectToAction("GerenciarAlunos", "Home");

        }
        #endregion

        #region Excluir Aluno
        public ActionResult ExcluirAluno(int IdAluno)
        {

            AlunoDAO.ExcluirAluno(IdAluno);
            return RedirectToAction("GerenciarAlunos", "Home");
        }
        #endregion

        #region Pág Mostrando as Disciplinas do Aluno
        public ActionResult DisciplinasAluno(int idAluno)
        {
            ViewBag.Mostrar = AlunoDAO.ListDisciplinaAluno(idAluno);
            List<Disciplina> disciplinas = ViewBag.Mostrar;
            if (disciplinas.Count != 0)
             { 
                ViewBag.NomeAluno = disciplinas.First().Aluno.Nome_Aluno;
             }
            return View(ViewBag.Mostrar);
        }

        #endregion

        #endregion

        #region CRUD das Disciplinas

        #region Pag Gerenciar Disciplina
        public ActionResult GerenciarDisciplinas()
        {
            return View(DisciplinaDAO.BuscarDisciplinas());
        }
        #endregion

        #region Pag Cadastrar Disciplina
        public ActionResult CadastrarDisciplina()
        {
            ViewBag.IdAluno = new SelectList(AlunoDAO.BuscarAlunos(), "IdAluno", "Nome_Aluno");
            return View();
        }
        #endregion

        #region Cadastrar Disciplina
        [HttpPost]
        public ActionResult CadastrarDisciplina(Disciplina disciplina)
        {
            ViewBag.IdAluno = new SelectList(AlunoDAO.BuscarAlunos(), "IdAluno", "Nome_Aluno");
            if (disciplina.NomeDisciplina.Equals("") || disciplina.IdAluno == 0)
            {
                ModelState.AddModelError("", "O Nome da Disciplina e o aluno não podem ser nulos!");
                return View(disciplina);
            }
            else
            {
                DisciplinaDAO.CadastrarDisciplina(disciplina);
                return RedirectToAction("GerenciarDisciplinas", "Home");
            }
        }
        #endregion

        #region Pág Editar Disciplina
        public ActionResult EditarDisciplina(int id)
        {
            ViewBag.IdAluno = new SelectList(AlunoDAO.BuscarAlunos(), "IdAluno", "Nome_Aluno");
            return View(DisciplinaDAO.BuscarDisciplinaById(id));
        }
        #endregion

        #region Edição da Disciplina
        [HttpPost]
        public ActionResult EditarDisciplina(Disciplina disciplinaNovo)
        {
            ViewBag.IdAluno = new SelectList(AlunoDAO.BuscarAlunos(), "IdAluno", "Nome_Aluno");
            if (disciplinaNovo.IdAluno == 0 || disciplinaNovo.NomeDisciplina == null)
            {
                ModelState.AddModelError("", "Todos os campos são obrigatórios");
                return RedirectToAction("Index", "Home");
            }

            Disciplina disciplinaOriginal = DisciplinaDAO.BuscarDisciplinaById(disciplinaNovo.IdDisciplina);
            disciplinaOriginal.NomeDisciplina = disciplinaNovo.NomeDisciplina;
            disciplinaOriginal.IdAluno = disciplinaNovo.IdAluno;
            DisciplinaDAO.EditarDisciplina(disciplinaOriginal);

            return RedirectToAction("GerenciarDisciplinas", "Home");

        }
        #endregion

        #region Excluir Disciplina
        public ActionResult ExcluirDisciplina(int id)
        {
            DisciplinaDAO.DeletarDisciplina(id);
           return RedirectToAction("GerenciarDisciplinas", "Home"); ;
        }
        #endregion

        #endregion

    }
}