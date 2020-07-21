using Stimulsoft.Report.Components;

namespace stimulTest.ReportGenerator
{
    public class PageTextProperties : BaseTextProperties<IPageHeaderTextBox>, ITextProperties<IPageHeaderTextBox>
    {
        private readonly IPageHeaderTextBox _builder;
        private readonly StiPageHeaderBand _band;
        
        public PageTextProperties(IPageHeaderTextBox builder, StiPageHeaderBand band, StiText currentText) : base(currentText)
        {
            _builder = builder;
            _band = band;
        }

        public IPageHeaderTextBox Then()
        {
            _band.Components.Add(CurrentText);
            return _builder;
        }
    }
}