using Newtonsoft.Json;
using System;
using System.Diagnostics;
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
            var testingObject = GetTestingObject();
            
            using (var stream = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(scenario.given))))
            {
                var sw = Stopwatch.StartNew();
                var result = testingObject.Analyse(stream);
                sw.Stop();

                AlgorithmResultValidator.AssertAreEquivalents(scenario.expected, result, ValidateText());
                
                Console.WriteLine($"Algorithm: {testingObject.GetType().Name} | ElapsedTime: {sw.ElapsedMilliseconds} ms");
            }
        }

        protected abstract ITextAnalyserAlgorithms GetTestingObject();

        protected abstract bool ValidateText();
    }
}
