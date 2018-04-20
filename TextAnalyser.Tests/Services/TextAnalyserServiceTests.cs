using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;
using TextAnalyser.Domain.Entities;
using TextAnalyser.Domain.Interfaces.Algorithms;
using TextAnalyser.Domain.Interfaces.Repositories;
using TextAnalyser.Domain.Interfaces.Serializers;
using TextAnalyser.Domain.ValueObjects;
using TextAnalyser.Services;

namespace TextAnalyser.Tests.Services
{
    [TestClass]
    public class TextAnalyserServiceTests
    {
        TextAnalyserService _testingObject;

        Mock<IAnalysesRepository> _mockRepository;
        Mock<IAnalyseResultSerializer> _mockSerializer;
        Mock<ITextAnalyserAlgorithms> _mockAlgorithm;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IAnalysesRepository>();
            _mockSerializer = new Mock<IAnalyseResultSerializer>();
            _mockAlgorithm = new Mock<ITextAnalyserAlgorithms>();

            _testingObject = new TextAnalyserService(
                repository: _mockRepository.Object,
                serializer: _mockSerializer.Object,
                algorithm: _mockAlgorithm.Object);
        }

        [TestMethod]
        public void ShouldAttendTheServiceFlow()
        {
            var sentences = new[] { new Sentence(null, 1, "1") };
            var algorithmResult = new AlgorithmResult(sentences);
            var serializationResult = Guid.NewGuid().ToString();

            _mockAlgorithm
                .Setup(m => m.Analyse(It.IsAny<StreamReader>()))
                .Returns(algorithmResult)
                .Verifiable();

            _mockSerializer
                .Setup(m => m.Serialize(It.IsAny<Analyse>()))
                .Callback(new Action<Analyse>(a => Validations.SentenceValidator.AssertAreEquivalents(sentences, a.Sentences, true)))
                .Returns(serializationResult)
                .Verifiable();

            _mockRepository
                .Setup(m => m.SaveAnalyse(It.IsAny<Analyse>()))
                .Callback(new Action<Analyse>(a => Validations.SentenceValidator.AssertAreEquivalents(sentences, a.Sentences, true)))
                .Returns(1)
                .Verifiable();

            var tempFile = Path.GetTempFileName();
            var result = _testingObject.Analyse(new AnalyseCommand(tempFile));

            Assert.AreEqual(serializationResult, result.Result);
            _mockRepository.VerifyAll();
            _mockSerializer.VerifyAll();
            _mockAlgorithm.VerifyAll();
        }
    }
}