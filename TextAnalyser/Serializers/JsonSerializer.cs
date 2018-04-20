using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;
using TextAnalyser.Domain.Entities;
using TextAnalyser.Domain.Interfaces.Serializers;
using TextAnalyser.Presenters;

namespace TextAnalyser.Serializers
{
    public class JsonSerializer : IAnalyseResultSerializer
    {
        public string Serialize(Analyse analyse)
        {
            if (analyse == null) return null;

            var outbound = new OutboundAnalysePresenter
            {
                sentenceCount = analyse.SentencesCount,
                wordBreakDown = analyse.Sentences.Select(s => new OutboundWordBreakDownPresenter
                {
                    wordCount = s.WordsCount,
                    sentenceNumber = s.SentenceNumber,
                    lettersBreakDown = s.Words.Select(w => new OutboundLettersBreakDown
                    {
                        lettersCount = w.LettersCount,
                        wordNumber = w.WordNumber
                    })
                })
            };
            
            return JsonConvert.SerializeObject(outbound, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        public override string ToString() => nameof(JsonSerializer);
    }
}