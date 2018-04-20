namespace TextAnalyser.Domain.Entities
{

    public class Word
    {
        public int Id { get; private set; }
        public int SentenceId { get; private set; }
        public int WordNumber { get; private set; }
        public string WordText { get; private set; }
        public int LettersCount { get; private set; }

        public Word(string wordText, int lettersCount, int wordNumber)
        {
            this.WordText = wordText;
            this.LettersCount = lettersCount;
            this.WordNumber = wordNumber;
        }

        protected Word() { }

        public override string ToString()
        {
            return $"Word [ {Id} | {SentenceId} | {WordNumber} | {LettersCount} | {WordText} ";
        }
    }
}