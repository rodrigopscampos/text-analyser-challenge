using System;
using System.Diagnostics;
using System.IO;
using TextAnalyser.Domain.Entities;
using TextAnalyser.Domain.Interfaces.Algorithms;
using TextAnalyser.Domain.Interfaces.Repositories;
using TextAnalyser.Domain.Interfaces.Serializers;
using TextAnalyser.Domain.ValueObjects;

namespace TextAnalyser.Services
{
    public class TextAnalyserService
    {
        private readonly IAnalysesRepository _repository;
        private readonly IAnalyseResultSerializer _serializer;
        private readonly ITextAnalyserAlgorithms _algorithm;

        public TextAnalyserService(
            IAnalysesRepository repository,
            IAnalyseResultSerializer serializer,
            ITextAnalyserAlgorithms algorithm
            )
        {
            _repository = repository;
            _serializer = serializer;
            _algorithm = algorithm;
        }

        public AnalyseResult Analyse(AnalyseCommand cmd)
        {
            TimeSpan timeToAnalyse;
            TimeSpan timeToSaveOnDatabase;
            TimeSpan timeToSerializeOutput;

            using (var stream = new StreamReader(cmd.TextFileAddress))
            {
                var sw = Stopwatch.StartNew();
                var algorithmResult = _algorithm.Analyse(stream);

                timeToAnalyse = sw.Elapsed;

                var textContent = File.ReadAllText(cmd.TextFileAddress);

                var analyse = new Analyse(
                    textFileAddress: cmd.TextFileAddress,
                    textFileContent: textContent,
                    sentences: algorithmResult.Sentences);


                sw.Restart();
                _repository.SaveAnalyse(analyse);
                timeToSaveOnDatabase = sw.Elapsed;

                sw.Restart();
                var result = _serializer.Serialize(analyse);
                timeToSerializeOutput = sw.Elapsed;

                return new AnalyseResult(
                    result: result,
                    timeToAnalyse: timeToAnalyse,
                    timeToSaveOnDatabase: timeToSaveOnDatabase,
                    timeToSerializeOutput: timeToSerializeOutput);
            }
        }
    }
}