namespace stimulTest.ReportGenerator
{
    public interface IPageHeaderTextBox
    {
        ITextProperties<IPageHeaderTextBox> WithText(string text = "");
        IReportBuilder Build();
    }
}