using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TesteDesenvolvedor.Models
{
    [Table("Disciplina")]
    public class Disciplina
    {
        [Key]
        public int IdDisciplina { get; set; }

        [ForeignKey("IdAluno")]
        public int IdAluno { get; set; }

        public string NomeDisciplina { get; set; }

        public virtual Aluno Aluno { get; set; }
    }
}