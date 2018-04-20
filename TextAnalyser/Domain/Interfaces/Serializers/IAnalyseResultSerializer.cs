using TextAnalyser.Domain.Entities;

namespace TextAnalyser.Domain.Interfaces.Serializers
{
    public interface IAnalyseResultSerializer
    {
        string Serialize(Analyse analyse);
    }
}
