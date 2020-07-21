using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Stimulsoft.Base.Drawing;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Events;

namespace stimulTest.ReportGenerator
{
    public class ReportBuilder : ReportBuilderBase, IReportBuilder
    {
        private readonly ITextBoxBuilder<StiText> _textBuilder;
        public ReportBuilder(StiPageOrientation orientation)
        {
            CreateReport(orientation);
            _textBuilder = new TextBoxBuilder(); 
        }

        public IReportBuilder WithOrientation(StiPageOrientation orientation)
        {
            SetOrientation(orientation);
            return this;
        }

        public IReportBuilder WithDataSource(object data, string dataSetName = "dataset")
        {
            Report.Dictionary.Databases.Clear();
            Report.RegData(dataSetName, data);
            Report.Dictionary.Synchronize();
            
            var props = data.GetType().GetGenericArguments().Single().GetProperties();
            foreach (var itm in props)
            {
                var align = TextAlign.Right;
                if (itm.PropertyType == typeof(string))
                    align = TextAlign.Left;
                Fields.Add(new Tuple<string, Type, TextAlign>(itm.Name, itm.PropertyType, align));
            }
            return this;
        }

        public IReportBuilder WithDataBand(List<string> fields, double[] sizes, List<TextAlign> aligns = null, string dataSetName = "dataset", double height = 0.2)
        {
            CurrentData = new StiDataBand(MakeRectangle(0, 0, 0, height))
            {
                DataSourceName = dataSetName
            };
            
            var condition = new StiCondition
            {
                BackColor = Color.FromArgb(0),
                TextColor = Color.Transparent,
                Expression = "(Line & 1) == 1",
                Item = StiFilterItem.Expression
            };

            CurrentData.Conditions.Add(condition);

            Page.Components.Add(CurrentData);


            var newVariable = new StiVariable()
            {
                Name = "Xdec",
                Alias = "Xdec",
                Value = "10",
                Type = typeof(int)
            };
            Report.Dictionary.Variables.Add(newVariable);


            CurrentData.BeforePrintEvent = new StiBeforePrintEvent("Xdec=Xdec+1;");
            CurrentData.AfterPrintEvent = new StiAfterPrintEvent("Xdec=Xdec+1;");


            
            var x = 0.0;
            for (var i = 0; i < fields.Count; i++)
            {
                TextAlign align;
                if (aligns != null)
                    align = aligns[i];
                else
                    align = Fields.FirstOrDefault(a => string.Equals(a.Item1, fields[i], StringComparison.CurrentCultureIgnoreCase))?.Item3 ?? TextAlign.Right;

                var text = "{" + $"{dataSetName}.{fields[i]}" + "}";
                var stiText = _textBuilder.New(text).WithFormat(MakeFormat(fields[i])).WithPosition(x)
                    .WithSize(sizes[i]).WithAlign((StiTextHorAlignment)align).Then();
                x += sizes[i];
                CurrentData.Components.Add(stiText);
            }

            return this;
        }

        public IReportBuilder WithDataBand(string[] fields, double[] sizes, List<TextAlign> aligns = null,
            string dataSetName = "dataset", double height = 0.2)
        {
            WithDataBand(fields.ToList(), sizes, aligns, dataSetName, height);
            return this;
        }

        public IReportBuilder WithPageHeaderBand(string title, string subTitle, double height = 0.2,bool showDatetime=true,bool showPaging=true)
        {
            CurrentPageHeader = new StiPageHeaderBand(MakeRectangle(0, 0, 0, height));
            Page.Components.Add(CurrentPageHeader);

            
            var stiTitle = _textBuilder.New(title).WithSize(Page.Width).WithAlign(StiTextHorAlignment.Center)
                .WithFont(FontStyle.Bold, 14).WithBorder(StiBorderSides.None).WithDockStyle(StiDockStyle.Top).Then();
            CurrentPageHeader.Components.Add(stiTitle);
            
            var stiSubTitle = _textBuilder.New(subTitle).WithPosition(0, 0.2).WithSize(Page.Width).WithAlign(StiTextHorAlignment.Center)
                .WithFont(FontStyle.Bold, 13).WithBorder(StiBorderSides.None).WithDockStyle(StiDockStyle.Top).Then();
            CurrentPageHeader.Components.Add(stiSubTitle);


            var xdec = _textBuilder.New("{Xdec}").WithPosition(0.2, 0.2).WithSize(2).WithAlign(StiTextHorAlignment.Center)
                .WithFont(FontStyle.Bold, 15).Then();
            CurrentPageHeader.Components.Add(xdec);


            CurrentPageHeader.BeforePrintEvent = new StiBeforePrintEvent("Xdec=Xdec+1;");
            CurrentPageHeader.AfterPrintEvent = new StiAfterPrintEvent("Xdec=Xdec+1;");



            if (showDatetime)
            {
                var top = CurrentPageHeader.Height - 0.2;
                var stiText = _textBuilder.New($"<b>Date/Time:</b> {DateTime.Now}").WithPosition(0, top).
                    WithSize(2).WithBorder(StiBorderSides.None).Then();
                CurrentPageHeader.Components.Add(stiText);
            }

            if (showPaging)
            {
                var top = CurrentPageHeader.Height - 0.2;
                var left = Page.Width - 2;
                var stiText = _textBuilder.New("{PageNofM}").WithPosition(left, top).WithSize(2).WithAlign(StiTextHorAlignment.Right)
                    .WithBorder(StiBorderSides.None).WithDockStyle(StiDockStyle.Top).Then();
                CurrentPageHeader.Components.Add(stiText);
            }

            return this;
        }
        public IPageHeaderTextBox WithPageHeaderBand(double height = 0.2)
        {
            return new PageHeaderTextBox(this, Page, height);
        }

        public IReportBuilder WithPageFooterBand(double height = 0.2)
        {
            CurrentPageFooter = new StiPageFooterBand(MakeRectangle(0, 0, 0, height));
            Page.Components.Add(CurrentPageFooter);

            CurrentPageFooter.BeforePrintEvent = new StiBeforePrintEvent("Xdec=Xdec+1;");
            CurrentPageFooter.AfterPrintEvent = new StiAfterPrintEvent("Xdec=Xdec+1;");

            return this;
        }

        public IReportBuilder WithReportTitleBand(Dictionary<string,string> properties,double height = 0.0)
        {
            ReportTitleBand = new StiReportTitleBand(MakeRectangle(0, 0, 0,height));
            Page.Components.Add(ReportTitleBand);

            var y = 0.0;
            foreach (var itm in properties)
            {
                var stiText = _textBuilder.New($"<b>{itm.Key}: </b>{itm.Value}").WithPosition(0, y).WithSize(ContentWidth)
                    .WithBorder(StiBorderSides.None).WithWordWrap(true).WithDockStyle(StiDockStyle.Top).Then();
                ReportTitleBand.Components.Add(stiText);
                y += 0.2;
            }
            ReportTitleBand.Height = y;

            ReportTitleBand.BeforePrintEvent = new StiBeforePrintEvent("Xdec=Xdec+1;");
            ReportTitleBand.AfterPrintEvent = new StiAfterPrintEvent("Xdec=Xdec+1;");


            var xdec = _textBuilder.New("{Xdec}").WithPosition(0.2, 0.2).WithSize(2).WithAlign(StiTextHorAlignment.Center)
                .WithFont(FontStyle.Bold, 15).Then();
            ReportTitleBand.Components.Add(xdec);
            return this;
        }
        
        public IReportBuilder WithHeaderBand(List<string> titles, double[] sizes, double height = 0.3)
        {
            CurrentHeader = new StiHeaderBand(MakeRectangle(0, 0, 0, height));
            Page.Components.Add(CurrentHeader);

            CurrentHeader.BeforePrintEvent = new StiBeforePrintEvent("Xdec=Xdec+1;");
            CurrentHeader.AfterPrintEvent = new StiAfterPrintEvent("Xdec=Xdec+1;");

            var x = 0.0;
            for (var i = 0; i < titles.Count; i++)
            {
                var stiText = _textBuilder.New(titles[i]).WithPosition(x).WithSize(sizes[i], height)
                    .WithAlign(StiTextHorAlignment.Center).WithFont(FontStyle.Bold).Then();
                x += sizes[i];
                CurrentHeader.Components.Add(stiText);
            }
            return this;
        }

        public IReportBuilder WithHeaderBand(string[] titles, double[] sizes, double height = 0.3)
        {
            WithHeaderBand(titles.ToList(), sizes, height);
            return this;
        }

        public IReportBuilder WithFooterBand(List<string> titles, double[] sizes,List<TextAlign> aligns=null, double height = 0.3)
        {
            CurrentFooter = new StiFooterBand(MakeRectangle(0, 0, 0, height));
            Page.Components.Add(CurrentFooter);

            var x = 0.0;
            for (var i = 0; i < titles.Count; i++)
            {
                var align = TextAlign.Right;
                if (aligns != null)
                    align = aligns[i];

                var stiText = _textBuilder.New(titles[i]).WithFormat(MakeFormat(titles[i])).WithPosition(x).WithSize(sizes[i])
                    .WithAlign((StiTextHorAlignment)align).WithFont(FontStyle.Bold).Then();
                x += sizes[i];
                CurrentFooter.Components.Add(stiText);
            }

            CurrentFooter.BeforePrintEvent = new StiBeforePrintEvent("Xdec=Xdec+1;");
            CurrentFooter.AfterPrintEvent = new StiAfterPrintEvent("Xdec=Xdec+1;");

            var xdec = _textBuilder.New("{Xdec}").WithPosition(0.2, 0.2).WithSize(2).WithAlign(StiTextHorAlignment.Center)
                .WithFont(FontStyle.Bold, 18).Then();
            CurrentFooter.Components.Add(xdec);
            return this;
        }

        public IReportBuilder WithFooterBand(string[] titles, double[] sizes, List<TextAlign> aligns = null, double height = 0.3)
        {
            WithFooterBand(titles.ToList(), sizes, aligns, height);
            return this;
        }

        public IReportBuilder WithGroupHeaderBand(string expression,List<string> groupTitles=null, double height = 0.2)
        {
            CurrentGroupHeader = new StiGroupHeaderBand(MakeRectangle(0, 0, 0, height))
            {
                Condition = {Value = expression}
            };
            Page.Components.Add(CurrentGroupHeader);

            var spaces = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            var titles = string.Join(spaces, groupTitles);

            var stiText = _textBuilder.New(titles).WithSize(ContentWidth, height).WithFont(FontStyle.Bold)
                .WithBorder(StiBorderSides.None).WithWordWrap(true).WithVAlign(StiVertAlignment.Bottom).Then();
            CurrentGroupHeader.Components.Add(stiText);
            return this;
        }

        public IReportBuilder WithGroupHeaderBand(string expression, string[] groupTitles = null, double height = 0.2)
        {
            WithGroupHeaderBand(expression, groupTitles?.ToList(), height);
            return this;
        }

        public IReportBuilder WithGroupFooterBand(List<string> titles, double[] sizes, List<TextAlign> aligns = null, double height = 0.3)
        {
            CurrentGroupFooter = new StiGroupFooterBand(MakeRectangle(0, 0, 0, height));
            Page.Components.Add(CurrentGroupFooter);

            var x = 0.0;
            for (var i = 0; i < titles.Count; i++)
            {
                var align = TextAlign.Right;
                if (aligns != null)
                    align = aligns[i];

                var stiText = _textBuilder.New(titles[i]).WithPosition(x).WithSize(sizes[i])
                    .WithAlign((StiTextHorAlignment)align).WithFont(FontStyle.Bold).Then();
                x += sizes[i];
                CurrentGroupFooter.Components.Add(stiText);
            }
            return this;
        }

        public IReportBuilder WithGroupFooterBand(string[] titles, double[] sizes, List<TextAlign> aligns = null, double height = 0.3)
        {
            WithGroupFooterBand(titles.ToList(), sizes, aligns, height);
            return this;
        }
        
        public StiReport Build() => Report;
        
    }
}