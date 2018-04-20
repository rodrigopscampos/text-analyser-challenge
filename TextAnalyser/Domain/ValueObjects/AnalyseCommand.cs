namespace TextAnalyser.Domain.ValueObjects
{
    public class AnalyseCommand
    {
        public string TextFileAddress { get; private set; }

        public AnalyseCommand(string textFileAddress)
        {
            this.TextFileAddress = textFileAddress;
        }
    }
}
