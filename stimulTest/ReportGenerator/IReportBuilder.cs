using System.Collections.Generic;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;

namespace stimulTest.ReportGenerator
{
    public interface IReportBuilder
    {
        IReportBuilder WithOrientation(StiPageOrientation orientation);
        IReportBuilder WithDataSource(object data, string dataSetName = "dataset");
        
        IReportBuilder WithPageHeaderBand(string title, string subTitle, double height = 0.2, bool showDatetime = true, bool showPaging = true);
        IPageHeaderTextBox WithPageHeaderBand(double height = 0.2);


        IReportBuilder WithPageFooterBand(double height = 0.2);

        IReportBuilder WithReportTitleBand(Dictionary<string, string> properties, double height = 0.0);
        

        IReportBuilder WithHeaderBand(List<string> titles, double[] sizes, double height = 0.3);
        IReportBuilder WithHeaderBand(string[] titles, double[] sizes, double height = 0.3);
        IReportBuilder WithFooterBand(List<string> titles, double[] sizes, List<TextAlign> aligns = null, double height = 0.3);
        IReportBuilder WithFooterBand(string[] titles, double[] sizes, List<TextAlign> aligns = null, double height = 0.3);


        IReportBuilder WithGroupHeaderBand(string expression, List<string> groupTitles = null, double height = 0.2);
        IReportBuilder WithGroupHeaderBand(string expression, string[] groupTitles = null, double height = 0.2);
        IReportBuilder WithGroupFooterBand(List<string> titles, double[] sizes, List<TextAlign> aligns = null, double height = 0.3);
        IReportBuilder WithGroupFooterBand(string[] titles, double[] sizes, List<TextAlign> aligns = null, double height = 0.3);


        IReportBuilder WithDataBand(List<string> fields, double[] sizes, List<TextAlign> aligns = null, string dataSetName = "dataset", double height = 0.2);
        IReportBuilder WithDataBand(string[] fields, double[] sizes, List<TextAlign> aligns = null, string dataSetName = "dataset", double height = 0.2);
        StiReport Build();
    }
}
