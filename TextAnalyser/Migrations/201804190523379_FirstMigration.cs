namespace TextAnalyser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Analyses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        TextFileAddress = c.String(),
                        TextFileContent = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sentences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SentenceNumber = c.Int(nullable: false),
                        SentenceText = c.String(),
                        WordsCount = c.Int(nullable: false),
                        Analyse_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Analyses", t => t.Analyse_Id)
                .Index(t => t.Analyse_Id);
            
            CreateTable(
                "dbo.Words",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SentenceId = c.Int(nullable: false),
                        WordNumber = c.Int(nullable: false),
                        WordText = c.String(),
                        LettersCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sentences", t => t.SentenceId, cascadeDelete: true)
                .Index(t => t.SentenceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sentences", "Analyse_Id", "dbo.Analyses");
            DropForeignKey("dbo.Words", "SentenceId", "dbo.Sentences");
            DropIndex("dbo.Words", new[] { "SentenceId" });
            DropIndex("dbo.Sentences", new[] { "Analyse_Id" });
            DropTable("dbo.Words");
            DropTable("dbo.Sentences");
            DropTable("dbo.Analyses");
        }
    }
}
