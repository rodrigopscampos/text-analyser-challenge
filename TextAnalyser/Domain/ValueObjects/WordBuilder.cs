using TextAnalyser.Domain.Entities;

namespace TextAnalyser.Domain.ValueObjects
{
    public class WordBuilder
    {
        public int WordNumber { get; private set; }
        public int LettersCount { get; private set; }

        public WordBuilder(int wordNumber)
        {
            WordNumber = wordNumber;
        }

        public void IncrementLettersCount()
            => LettersCount++;

        public Word Build() => new Word(null, LettersCount, WordNumber);
    }
}