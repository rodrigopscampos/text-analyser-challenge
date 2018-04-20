using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using TextAnalyser.Domain.Entities;
using TextAnalyser.Domain.Interfaces.Repositories;

namespace TextAnalyser.Infra.SqlServerRepository
{
    public class SqlServerAnalyseDbContext : DbContext, IAnalysesRepository
    {
        public IDbSet<Analyse> Analyses { get; set; }
        public IDbSet<Sentence> Sentences { get; set; }
        public IDbSet<Word> Words { get; set; }

        public int SaveAnalyse(Analyse analyse)
        {
            this.Analyses.Add(analyse);

            this.SaveChanges();

            return analyse.Id;
        }

        public Analyse GetAnalyseById(int id)
        {
            return Analyses.Find(id);
        }

        public SqlServerAnalyseDbContext()
            :base("DefaultConnection")
        {
         
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Analyse>().HasMany(a => a.Sentences);
            modelBuilder.Entity<Analyse>().Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Sentence>().Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Sentence>().HasMany(s => s.Words);

            base.OnModelCreating(modelBuilder);
        }

        public override string ToString() => "SqlServer";
    }
}