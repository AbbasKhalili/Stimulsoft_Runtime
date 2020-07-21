using Stimulsoft.Report.Components;

namespace stimulTest.ReportGenerator
{
    public class TextBoxBuilder : ElementBuilderBase, ITextBoxBuilder<StiText>
    {
        private StiText _currentText;

        public ITextProperties<StiText> New(string text="")
        {
            _currentText = MakeTextBox(text);
            return new TextProperties(_currentText);
        }
    }
}