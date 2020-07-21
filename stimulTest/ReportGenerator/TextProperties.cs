using Stimulsoft.Report.Components;

namespace stimulTest.ReportGenerator
{
    public class TextProperties : BaseTextProperties<StiText>, ITextProperties<StiText>
    {
        public TextProperties(StiText currentText) : base(currentText)
        {
        }

        public StiText Then() => CurrentText;
        
    }
}