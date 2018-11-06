using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TesteDesenvolvedor.Models
{
    [Table("Aluno")]
    public class Aluno
    {
        [Key]
        public int IdAluno { get; set; }

        public string Nome_Aluno { get; set; }
    }
}