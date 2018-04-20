using Newtonsoft.Json;
using System.IO;
using System.Text;
using TextAnalyser.Domain.Interfaces.Algorithms;
using TextAnalyser.Tests.Validations;

namespace TextAnalyser.Tests.Algorithms
{
    public abstract class GenericAlgorithmTest
    {
        protected void RunScenario(string scenarioJson)
        {
            var scenario = JsonConvert.DeserializeObject<AlgorithmScenario>(scenarioJson);

            using (var stream = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(scenario.given))))
            {
                var testingObject = GetTestingObject();
                var result = testingObject.Analyse(stream);
                AlgorithmResultValidator.AssertAreEquivalents(scenario.expected, result, ValidateText());
            }
        }

        protected abstract ITextAnalyserAlgorithms GetTestingObject();

        protected abstract bool ValidateText();
    }
}
