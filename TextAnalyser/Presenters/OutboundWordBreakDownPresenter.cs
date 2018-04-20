using System.Collections.Generic;

namespace TextAnalyser.Presenters
{
    public class OutboundWordBreakDownPresenter
    {
        public int sentenceNumber { get; set; }
        public int wordCount { get; set; }
        public IEnumerable<OutboundLettersBreakDown>  lettersBreakDown { get; set; }
    }
}