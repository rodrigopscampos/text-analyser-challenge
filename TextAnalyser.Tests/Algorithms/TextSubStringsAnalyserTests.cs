using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextAnalyser.Algorithms;
using TextAnalyser.Domain.Interfaces.Algorithms;

namespace TextAnalyser.Tests.Algorithms
{

    [TestClass]
    public class TextSubStringsAnalyserTests : GenericAlgorithmTest
    {
        TextSubStringsAlgorithm _testingObject;

        [TestInitialize]
        public void Setup()
        {
            _testingObject = new TextSubStringsAlgorithm();
        }

        [TestMethod]
        public void TextSubStringAnalyser_Scenario_001()
            => RunScenario(AlgorithmsScenarios.Algorithm_Scenario_001);

        [TestMethod]
        public void TextSubStringAnalyser_Scenario_002()
            => RunScenario(AlgorithmsScenarios.Algorithm_Scenario_002);

        [TestMethod]
        public void TextSubStringAnalyser_Scenario_003()
            => RunScenario(AlgorithmsScenarios.Algorithm_Scenario_003);

        [TestMethod]
        public void TextSubStringAnalyser_Scenario_004()
            => RunScenario(AlgorithmsScenarios.Algorithm_Scenario_004);

        protected override ITextAnalyserAlgorithms GetTestingObject() => _testingObject;
        protected override bool ValidateText() => true;
    }
}