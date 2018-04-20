using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TextAnalyser.Domain.Entities;
using TextAnalyser.Infra.SqlServerRepository;
using TextAnalyser.Tests.Validations;

namespace TextAnalyser.Tests.Infra.SqlServerRepository
{
    [TestClass]
    public class AnalysesDbContextTests
    {
        SqlServerAnalyseDbContext _testingObject;

        [TestInitialize]
        public void Setup()
        {
            _testingObject = new SqlServerAnalyseDbContext();
        }


        [TestMethod]
        public void ShouldSelectOnTables()
        {
            _testingObject.Sentences.ToArray();
            _testingObject.Analyses.ToArray();
            _testingObject.Words.ToArray();
        }

        [TestMethod]
        public void ShouldSaveAnalyse()
        {
            var analyse = new Analyse(
                textFileAddress: Guid.NewGuid().ToString(),
                textFileContent: Guid.NewGuid().ToString(),

                sentences: new[] { new Sentence(
                    sentenceNumber: 1,
                    sentenceText: "abc def",
                    words:
                        new []
                        {
                            new Word("abc", 3, 1),
                            new Word("def", 3, 2),
                        }
                    ) }
                );

            var id = _testingObject.SaveAnalyse(analyse);

            var result = _testingObject.GetAnalyseById(id);

          
            Assert.IsTrue(result.CreatedAt != DateTime.MinValue);
            AnalyserValidator.AssertAreEquivalents(analyse, result, true);
        }
    }
}