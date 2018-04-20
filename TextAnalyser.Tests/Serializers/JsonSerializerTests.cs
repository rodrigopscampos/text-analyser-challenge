using global::TextAnalyser.Serializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TextAnalyser.Domain.Entities;

namespace TextAnalyser.Tests.Serializers
{
    [TestClass]
    public class JsonSerializerTests
    {
        JsonSerializer _testingObject;

        [TestInitialize]
        public void Setup()
        {
            _testingObject = new JsonSerializer();
        }


        [TestMethod]
        public void ShouldSerializeAsExpected()
        {
            var textFileAddress = Guid.NewGuid().ToString();
            var textFileContent = "Text File Content";

            var analyse = new Analyse(
                textFileAddress: textFileAddress,
                textFileContent: textFileContent,
                sentences: new[]
                { new Sentence(
                    words: new [] { new Word("abc", 3, 1 )},
                    sentenceNumber: 1,
                    sentenceText: "abc"
                    )
                });

            var result = _testingObject.Serialize(analyse);

            Assert.AreEqual(JsonSerializerScenarios.JsonSerializer_Scenario_001, result);
        }
    }
}