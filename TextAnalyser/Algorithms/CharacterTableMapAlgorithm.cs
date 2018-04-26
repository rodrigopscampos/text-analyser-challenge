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

        const char EndOfSentenceIdentifier = '.';
        const char EndOfWordIdentifier = ' ';
        const int BufferSize = 1024 * 1024 * 1; //1 MB

        bool _endOfWord;
        bool _endOfSentence;

        int _sentenceNumber = 1;
        int _wordNumber = 1;
        IntRef _lettersCount = new IntRef(0);

        char[] buffer = new char[BufferSize];
        int _countRead;

        Dictionary<int, SentenceBuilder> _sentenceBuilders = new Dictionary<int, SentenceBuilder>();

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

            var sentences = _sentenceBuilders.Values.Select(s => s.Build()).ToArray();

            var result = new AlgorithmResult(sentences);

            ResetCounters();

            return result;
        }

        private void ProcessCharacter(char c, char next, bool lastCharacter)
        {

#if DEBUG
            debugTail.Append(c);
#endif

            if (c == EndOfSentenceIdentifier && !lastCharacter && !_endOfSentence)
            {
                _endOfSentence = true;

                AddEntryOnSentenceBuilder();

                _sentenceNumber++;
                _lettersCount = new IntRef(0);
                _wordNumber = 1;
                _endOfWord = false;
            }
            else if (c == EndOfWordIdentifier && !lastCharacter && !_endOfWord && !_endOfSentence)
            {
                _endOfWord = true;

                AddEntryOnSentenceBuilder();

                _wordNumber++;
                _lettersCount = new IntRef(0);
            }

            else if (c != EndOfSentenceIdentifier && c != EndOfWordIdentifier)
            {
                var ImACommaInTheMiddleOfSomeWord = (c == ',' && next != ' '); //propably some decimal number

                //Sorry, but this boolean expression is much faster than the Contains method of a collection, even a HashSet
                //var pontuations = new HashSet<char>(new[] { '!', '?', ',' });
                //var ImAPontuationCharacter = pontuations.Contains(c);

                var ImAPontuationCharacter = (c == '!' || c == '?' || c == ',');

                if (!ImAPontuationCharacter || ImACommaInTheMiddleOfSomeWord)
                {
                    _lettersCount++;

                    _endOfWord = false;
                    _endOfSentence = false;
                }
            }

            if (lastCharacter)
            {
                AddEntryOnSentenceBuilder();
            }
        }

        private void AddEntryOnSentenceBuilder()
        {
            SentenceBuilder sb;
            if (!_sentenceBuilders.TryGetValue(_sentenceNumber, out sb))
            {
                sb = new SentenceBuilder(_sentenceNumber);
                _sentenceBuilders.Add(_sentenceNumber, sb);
            }

            sb.AddWord(_wordNumber, _lettersCount.Value);
        }

        private void ResetCounters()
        {
            _endOfWord = false;
            _endOfSentence = false;

            _sentenceNumber = 1;
            _wordNumber = 1;
            _lettersCount = new IntRef(0);

            buffer = new char[BufferSize];
            _countRead = 0;

            _sentenceBuilders = new Dictionary<int, SentenceBuilder>();
        }

        public override string ToString() => nameof(CharacterTableMapAlgorithm);
    }
}
