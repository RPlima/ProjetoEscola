namespace TesteDesenvolvedor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aluno",
                c => new
                    {
                        IdAluno = c.Int(nullable: false, identity: true),
                        Nome_Aluno = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.IdAluno);
            
            CreateTable(
                "dbo.Disciplina",
                c => new
                    {
                        IdDisciplina = c.Int(nullable: false, identity: true),
                        NomeDisciplina = c.String(nullable: false, maxLength: 50),
                        IdAluno = c.Int(),
                    })
                .PrimaryKey(t => t.IdDisciplina)
                .ForeignKey("dbo.Aluno", t => t.IdAluno)
                .Index(t => t.IdAluno);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Disciplina", "IdAluno", "dbo.Aluno");
            DropIndex("dbo.Disciplina", new[] { "IdAluno" });
            DropTable("dbo.Disciplina");
            DropTable("dbo.Aluno");
        }
    }
}
