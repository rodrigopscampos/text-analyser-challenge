using System.Collections.Generic;

namespace TextAnalyser.Domain.Entities
{
    public class Sentence
    {
        public int Id { get; private set; }
        public int SentenceNumber { get; private set; }

        public string SentenceText { get; private set; }

        public int WordsCount { get; private set; }
        public virtual ICollection<Word> Words { get; private set; }

        public Sentence(ICollection<Word> words, int sentenceNumber, string sentenceText)
        {
            this.Words = words;
            this.WordsCount = words?.Count ?? 0;
            this.SentenceNumber = sentenceNumber;
            this.SentenceText = sentenceText;
        }

        protected Sentence() { }

        public override string ToString()
        {
            return $"Sentence [ {Id} | {SentenceNumber} | {WordsCount} | {SentenceText} ";
        }
    }
}