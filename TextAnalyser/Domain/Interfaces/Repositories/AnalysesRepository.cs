using TextAnalyser.Domain.Entities;

namespace TextAnalyser.Domain.Interfaces.Repositories
{
    public interface IAnalysesRepository
    {
        int SaveAnalyse(Analyse analyze);
        Analyse GetAnalyseById(int id);
    }
}
