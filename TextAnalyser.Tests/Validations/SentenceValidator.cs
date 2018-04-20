using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using TextAnalyser.Domain.Entities;

namespace TextAnalyser.Tests.Validations
{
    public static class SentenceValidator
    {
        public static void AssertAreEquivalents(Sentence expected, Sentence result, bool validateText)
        {
            Assert.AreEqual(expected.SentenceNumber, result.SentenceNumber);
            Assert.AreEqual(expected.WordsCount, result.WordsCount);
            if (validateText) Assert.AreEqual(expected.SentenceText, result.SentenceText);

            WordValidator.AssertAreEquivalents(expected.Words, result.Words, validateText);
        }

        public static void AssertAreEquivalents(IEnumerable<Sentence> expected, IEnumerable<Sentence> result, bool validateText)
        {
            Assert.AreEqual(expected.Count(), result.Count());

            var expectedAsArray = expected.ToArray();
            var resultAsArray = result.ToArray();

            for (int i = 0; i < expectedAsArray.Length; i++)
                AssertAreEquivalents(expectedAsArray[i], resultAsArray[i], validateText);
        }
    }
}
