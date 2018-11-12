using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TesteDesenvolvedor.Models
{
    public class Context : DbContext
    {
        public Context() : base("Escola") { }

        public DbSet<Aluno> Aluno { get; set; }
        public DbSet<Disciplina> Disciplina { get; set; }
    }
}