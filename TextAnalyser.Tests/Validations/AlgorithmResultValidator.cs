using TextAnalyser.Domain.ValueObjects;

namespace TextAnalyser.Tests.Validations
{
    public static class AlgorithmResultValidator
    {
        public static void AssertAreEquivalents(AlgorithmResult expected, AlgorithmResult result, bool validateText)
        {
            if (expected == null && result == null) return;

            SentenceValidator.AssertAreEquivalents(expected.Sentences, result.Sentences, validateText);
        }
    }
}
