using System.Collections.Generic;
using System.IO;
using System.Linq;
using TextAnalyser.Domain.Entities;
using TextAnalyser.Domain.Interfaces.Algorithms;
using TextAnalyser.Domain.ValueObjects;

namespace TextAnalyser.Algorithms
{
    /// <summary>
    /// This algorithm extract number of sentences, number of words in each sentences and number of letters in each word.
    /// Its name is "TextSubStringsAlgorithm", because it runs down splitting and analysing the substrings to extract those info
    /// This algorithm is capable to save the saave strings patchs, like sentences and words
    /// </summary>
    public class TextSubStringsAlgorithm : ITextAnalyserAlgorithms
    {
        static readonly char[] Pontuations = new[] { '!', '?', ',' };

        public AlgorithmResult Analyse(StreamReader textReader)
        {
            var text = textReader.ReadToEnd();

            var sentences = ExtractSentencesFromText(text)
                .Select((s, i) => CreateSentenceFromText(s, i + 1))
                .ToArray();

            return new AlgorithmResult(sentences);
        }

        private static IEnumerable<string> ExtractSentencesFromText(string text)
        {
            return text
                .Split('.')
                .Where(t => !string.IsNullOrWhiteSpace(t));
        }

        private static Sentence CreateSentenceFromText(string text, int sentenceNumber)
        {
            var words = ExtractWordsFromText(text)
                            .Select((w, i) => new Word(w, GetWordLength(w), i + 1))
                            .ToArray();

            return new Sentence(words, sentenceNumber, text);
        }

        private static int GetWordLength(string word)
        {
            //
            // This method checks and subtract possible pontuation character at the begining or end of the word
            // example: in the sentence "How are you?", the last word is "you", not "you?"
            //

            if (word == null || word.Length == 0) return 0;

            var adjust = 0;
            if (Pontuations.Contains(word.First())) adjust++;
            if (word.Length > 1 && Pontuations.Contains(word.Last())) adjust++;

            return word.Length - adjust;
        }

        private static IEnumerable<string> ExtractWordsFromText(string text)
        {
            return text
                .Split(' ')
                .Where(w => !string.IsNullOrWhiteSpace(w));
        }

        public override string ToString() => nameof(TextSubStringsAlgorithm);
    }
}