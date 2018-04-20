using System;
using System.Text;

namespace TextAnalyser.Domain.ValueObjects
{
    public class AnalyseResult
    {
        public string Result { get; private set; }
        public TimeSpan TimeToAnalyse { get; private set; }
        public TimeSpan TimeToSaveOnDatabase { get; private set; }
        public TimeSpan TimeToSerializeOutput { get; private set; }

        public AnalyseResult(
            string result,
            TimeSpan timeToAnalyse,
            TimeSpan timeToSaveOnDatabase,
            TimeSpan timeToSerializeOutput
            )
        {
            this.Result = result;
            this.TimeToAnalyse = timeToAnalyse;
            this.TimeToSaveOnDatabase = timeToSaveOnDatabase;
            this.TimeToSerializeOutput = timeToSerializeOutput;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("Result: ");
            sb.AppendLine(Result);

            sb.AppendLine();
            sb.AppendLine("Statistics: ");

            sb.AppendLine($"Time to run analyse     : {TimeToAnalyse}");
            sb.AppendLine($"Time to save on Database: {TimeToSaveOnDatabase}");
            sb.AppendLine($"Time to serialize Output: {TimeToSerializeOutput}");

            return sb.ToString();
        }
    }
}
