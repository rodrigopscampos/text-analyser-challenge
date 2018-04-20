using System.Collections.Generic;
using System.IO;
using System.Linq;
using TextAnalyser.Domain.Interfaces.Algorithms;
using TextAnalyser.Domain.ValueObjects;

namespace TextAnalyser.Algorithms
{
    /// <summary>
    /// This algorithm extract number of sentences, number of words in each sentences and number of letters in each word.
    /// Its name is "CharacterTableMapAlgorithm", because it runs a StreamReader, check each caracter and map it according to the value
    /// The mapping process, means to analyse the char and insert into matrix. The columns represents the position (Sentence Number and Word Number)
    /// The Sentence Count and Word Count are accquired incrementing a Int32 reference type, internaly implemented
    /// This algorithm is very efficient, because it runs only once over the string
    /// This algorithm is not capable to save strings patchs, like sentences and words
    /// Furthermore, this algorithm avoids to load all string to memory. It reads in blocks of 1 MB
    /// </summary>
    public class CharacterTableMapAlgorithm : ITextAnalyserAlgorithms
    {
        /// <summary>
        /// This class simulate a Int32 reference type for the local necessity
        /// </summary>
        internal class IntRef
        {
            internal int Value { get; private set; }
            internal IntRef(int i) { Value = i; }
            public override string ToString() => Value.ToString();
            public static IntRef operator ++(IntRef i) { i.Value++; return i; }
        }

        internal class TableEntry
        {
            readonly internal IntRef SentenceCount;
            readonly internal int SentenceNumber;
            readonly internal IntRef WordsCount;
            readonly internal int WordNumber;
            readonly internal IntRef LettersCount;

            internal TableEntry(IntRef sentenceCount, int sentenceNumber, IntRef wordsCount, int wordNumber, IntRef lettersCount)
            {
                this.SentenceCount = sentenceCount;
                this.SentenceNumber = sentenceNumber;
                this.WordsCount = wordsCount;
                this.WordNumber = wordNumber;
                this.LettersCount = lettersCount;
            }

            public override string ToString()
            {
                return $"( {SentenceCount} | {SentenceNumber} | {WordsCount} | {WordNumber} | {LettersCount} )";
            }
        }

        char[] Pontuations = new[] { '!', '?', ',' };

        const char EndOfSentenceIdentifier = '.';
        const char EndOfWordIdentifier = ' ';
        const int BufferSize = 1024 * 1024 * 1; //1 MB

        bool _endOfWord;
        bool _endOfSentence;

        IntRef _sentenceCount = new IntRef(0);
        int _sentenceNumber = 1;
        IntRef _wordsCount = new IntRef(1);
        int _wordNumber = 1;
        IntRef _lettersCount = new IntRef(0);

        List<TableEntry> _table = new List<TableEntry>();

        char[] buffer = new char[BufferSize];
        int _countRead;

#if DEBUG
        //Just to help us to debug
        System.Text.StringBuilder debugTail = new System.Text.StringBuilder();
#endif

        public AlgorithmResult Analyse(StreamReader textReader)
        {
            var c = default(char);
            var next = default(char);
            var aux = default(char);

            while ((_countRead = textReader.ReadBlock(buffer, 0, BufferSize)) > 0)
            {
                if (aux != default(char))
                    ProcessCharacter(aux, buffer[0], false);

                for (int i = 0; i < _countRead; i++)
                {
                    c = buffer[i];
                    if (i + 1 < _countRead)
                    {
                        next = buffer[i + 1];
                    }
                    else if (!textReader.EndOfStream)
                    {
                        //
                        // It's necessary to know the next charater to handle correcty decimal numbers
                        // To garantee, the knologement of the next character, when the cursor in at i - 1 from the end of the buffer
                        // we copy c to aux and refill the buffer. Consequently, the next char will go to buffer[0]
                        //

                        aux = c;
                        break;
                    }

                    var lastCharacter = i == _countRead - 1 && textReader.EndOfStream;
                    ProcessCharacter(c, next, lastCharacter);
                }
            }
            
            var sentenceBuilders = new Dictionary<int, SentenceBuilder>();
            foreach (var entry in _table)
            {
                if (!sentenceBuilders.ContainsKey(entry.SentenceNumber))
                    sentenceBuilders.Add(entry.SentenceNumber, new SentenceBuilder(entry.SentenceNumber));

                sentenceBuilders[entry.SentenceNumber].AddWord(entry.WordNumber, entry.LettersCount.Value);
            }

            var sentences = sentenceBuilders.Values.Select(s => s.Build());

            return new AlgorithmResult(sentences);
        }

        private void ProcessCharacter(char c, char next, bool lastCharacter)
        {

#if DEBUG
            debugTail.Append(c);
#endif

            if (c == EndOfSentenceIdentifier && !lastCharacter && !_endOfSentence)
            {
                _endOfSentence = true;

                _table.Add(new TableEntry(_sentenceCount, _sentenceNumber, _wordsCount, _wordNumber, _lettersCount));

                _sentenceCount++;
                _sentenceNumber++;
                _wordsCount = new IntRef(1);
                _lettersCount = new IntRef(0);
                _wordNumber = 1;
                _endOfWord = false;
            }
            else if (c == EndOfWordIdentifier && !lastCharacter && !_endOfWord && !_endOfSentence)
            {
                _endOfWord = true;

                _table.Add(new TableEntry(_sentenceCount, _sentenceNumber, _wordsCount, _wordNumber, _lettersCount));

                _wordsCount++;
                _wordNumber++;
                _lettersCount = new IntRef(0);
            }

            else if (c != EndOfSentenceIdentifier && c != EndOfWordIdentifier)
            {
                var ImACommaInTheMiddleOfSomeWord = (c == ',' && next != ' '); //propably some decimal number

                if (!Pontuations.Contains(c) || ImACommaInTheMiddleOfSomeWord)
                {
                    _lettersCount++;

                    _endOfWord = false;
                    _endOfSentence = false;
                }
            }

            if (lastCharacter)
            {
                _table.Add(new TableEntry(_sentenceCount, _sentenceNumber, _wordsCount, _wordNumber, _lettersCount));
            }
        }

        public override string ToString() => nameof(CharacterTableMapAlgorithm);
    }
}
