using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextAnalyser.Domain.Entities;

namespace TextAnalyser.Tests.Validations
{
    public static class AnalyserValidator
    {
        public static void AssertAreEquivalents(Analyse expected, Analyse result, bool validateText)
        {
            Assert.AreEqual(expected.SentencesCount, result.SentencesCount);
            Assert.AreEqual(expected.TextFileAddress, result.TextFileAddress);
            Assert.AreEqual(expected.TextFileContent, result.TextFileContent);

            SentenceValidator.AssertAreEquivalents(expected.Sentences, result.Sentences, validateText);
        }
    }
}
