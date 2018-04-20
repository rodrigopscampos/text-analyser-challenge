using System;
using System.Collections.Generic;
using System.Linq;

namespace TextAnalyser.Domain.Entities
{
    public class Analyse
    {
        public int Id { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public string TextFileAddress { get; private set; }
        public string TextFileContent { get; private set; }

        public int SentencesCount => Sentences?.Count() ?? 0;
        public virtual ICollection<Sentence> Sentences { get; set; }

        public Analyse(string textFileAddress, string textFileContent, IEnumerable<Sentence> sentences)
        {
            this.Sentences = sentences?.ToArray();
            this.TextFileAddress = textFileAddress;
            this.TextFileContent = textFileContent;
        }

        protected Analyse() { }

        public override string ToString()
        {
            return $"Analyse [ {Id} | {CreatedAt} | {SentencesCount} | {TextFileAddress} | {TextFileContent} ]";
        }
    }
}