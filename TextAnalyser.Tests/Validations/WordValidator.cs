using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using TextAnalyser.Domain.Entities;

namespace TextAnalyser.Tests.Validations
{
    public static class WordValidator
    {
        public static void AssertAreEquivalents(Word expected, Word result, bool validateText)
        {
            Assert.AreEqual(expected.LettersCount, result.LettersCount);
            Assert.AreEqual(expected.WordNumber, result.WordNumber);
            if (validateText) Assert.AreEqual(expected.WordText, result.WordText);
        }

        public static void AssertAreEquivalents(IEnumerable<Word> expected, IEnumerable<Word> result, bool validateText)
        {
            if (expected == null && result == null) return;

            Assert.AreEqual(expected.Count(), result.Count());

            var expectedWordsArray = expected.ToArray();
            var resultWordsArray = result.ToArray();

            for (int i = 0; i < expectedWordsArray.Length; i++)
                AssertAreEquivalents(expectedWordsArray[i], resultWordsArray[i], validateText);
        }
    }
}
