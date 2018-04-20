using System.Collections.Generic;
using TextAnalyser.Domain.Entities;

namespace TextAnalyser.Domain.ValueObjects
{
    public class AlgorithmResult
    {
        public IEnumerable<Sentence> Sentences { get; private set; }

        public AlgorithmResult(IEnumerable<Sentence> sentences)
        {
            this.Sentences = sentences;
        }
    }
}