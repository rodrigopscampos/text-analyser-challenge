using System.Collections.Generic;

namespace TextAnalyser.Presenters
{
    public class OutboundAnalysePresenter
    {
        public int sentenceCount { get; set; }
        public IEnumerable<OutboundWordBreakDownPresenter> wordBreakDown { get; set; }
    }
}