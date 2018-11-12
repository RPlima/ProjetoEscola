namespace TesteDesenvolvedor.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Disciplina")]
    public partial class Disciplina
    {
        [Key]
        public int IdDisciplina { get; set; }

        [Required]
        [StringLength(50)]
        public string NomeDisciplina { get; set; }

        public int? IdAluno { get; set; }

        public virtual Aluno Aluno { get; set; }
    }
}
