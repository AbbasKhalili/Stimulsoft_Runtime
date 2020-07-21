using Stimulsoft.Base.Drawing;
using Stimulsoft.Report.Components;

namespace stimulTest.ReportGenerator
{
    public class PageHeaderTextBox : ElementBuilderBase, IPageHeaderTextBox
    {
        private readonly IReportBuilder _reportBuilder;
        protected StiPageHeaderBand CurrentPageHeader;

        public PageHeaderTextBox(IReportBuilder reportBuilder, StiPage page, double height = 0.2)
        {
            _reportBuilder = reportBuilder;
            CurrentPageHeader = new StiPageHeaderBand(new RectangleD(0, 0, 0, height));
            page.Components.Add(CurrentPageHeader);
        }

        public ITextProperties<IPageHeaderTextBox> WithText(string text="")
        {
            var currentText = MakeTextBox(text);
            var properties = new PageTextProperties(this, CurrentPageHeader, currentText);
            return properties;
        }

        public IReportBuilder Build()
        {
            return _reportBuilder;
        }
    }
}