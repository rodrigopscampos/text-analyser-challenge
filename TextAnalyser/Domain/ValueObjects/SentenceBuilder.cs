using System.Collections.Generic;
using System.Linq;
using TextAnalyser.Domain.Entities;



namespace TextAnalyser.Domain.ValueObjects
{
    public class SentenceBuilder
    {
        public int SentenceNumber { get; private set; }

        private Dictionary<int, Word> _wordsBuilder
            = new Dictionary<int, Word>();

        public SentenceBuilder(int sentenceNumber)
        {
            this.SentenceNumber = sentenceNumber;
        }

        public void AddWord(int wordNumber, int letterCount)
            => _wordsBuilder.Add(wordNumber, new Word(null, letterCount, wordNumber));

        public Sentence Build()
            => new Sentence(
                words: _wordsBuilder.Values.ToArray(),
                sentenceNumber: SentenceNumber,
                sentenceText: null);
    }
}
