using System.IO;
using TextAnalyser.Domain.ValueObjects;

namespace TextAnalyser.Domain.Interfaces.Algorithms
{
    public interface ITextAnalyserAlgorithms
    {
        AlgorithmResult Analyse(StreamReader textReader);
    }
}
