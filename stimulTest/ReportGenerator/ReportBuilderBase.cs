using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using Stimulsoft.Base.Drawing;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.Components.TextFormats;
using Stimulsoft.Report.Units;

namespace stimulTest.ReportGenerator
{
    public abstract class ElementBuilderBase
    {
        protected StiText MakeTextBox(string text = "")
        {
            return new StiText(MakeRectangle(0, 0, 0, 0))
            {
                Text = text.Replace("~", "<br/>").Replace("$",""),
                AllowHtmlTags = true,
                Border = MakeBorder (StiBorderSides.All),
                HorAlignment = StiTextHorAlignment.Left,
                VertAlignment = StiVertAlignment.Center,
                Width = 1,
                Height = 0.2,
                WordWrap = false,
                TextFormat = MakeFormat(text)
            };
        }

        protected StiFormatService MakeFormat(string text)
        {
            StiFormatService textFormat;
            if (text.Length > 0 && text[0] == '$')
                textFormat = new StiCurrencyFormatService();
            else
                textFormat = new StiGeneralFormatService();

            return textFormat;
        }
        protected RectangleD MakeRectangle(double x, double y, double width, double height)
        {
            return new RectangleD(x, y, width, height);
        }

        protected Font MakeFont(string fontName, float size, FontStyle fontStyle)
        {
            return new Font(new FontFamily(fontName), size, fontStyle);
        }

        protected StiSolidBrush MakeBrush(Color color)
        {
            return new StiSolidBrush(color);
        }

        protected StiBorder MakeBorder(StiBorderSides side)
        {
            return new StiBorder() { Side = side, Color = Color.Black };
        }
    }
    public abstract class ReportBuilderBase: ElementBuilderBase
    {
        protected StiReport Report;
        protected StiPage Page;
        
        protected StiPageHeaderBand CurrentPageHeader;
        protected StiPageFooterBand CurrentPageFooter;

        protected StiHeaderBand CurrentHeader;
        protected StiFooterBand CurrentFooter;

        protected StiGroupHeaderBand CurrentGroupHeader;
        protected StiGroupFooterBand CurrentGroupFooter;

        protected StiDataBand CurrentData;
        public List<Tuple<string, Type, TextAlign>> Fields;

        protected StiReportTitleBand ReportTitleBand;

        protected double ContentWidth = 7.48;
        protected double ContentHeight = 10.90;

        protected ReportBuilderBase()
        {
            Fields = new List<Tuple<string, Type, TextAlign>>();
        }

        protected void CreateReport(StiPageOrientation orientation)
        {
            Report = new StiReport {ReportUnit = StiReportUnitType.Inches};
            Page = Report.Pages[0];
            Page.PaperSize = PaperKind.A4;
            Page.ReportUnit = StiUnit.Inches;
            SetOrientation(orientation);
        }

        protected void SetOrientation(StiPageOrientation orientation)
        {
            Page.Orientation = orientation;
            CalculateContentSize(orientation);
        }

        private void CalculateContentSize(StiPageOrientation orientation)
        {
            if (orientation == StiPageOrientation.Portrait) return;
            ContentWidth = 10.90;
            ContentHeight = 7.48;
        }
    }
}