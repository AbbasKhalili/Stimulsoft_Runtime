using System.Drawing;
using Stimulsoft.Base.Drawing;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.Components.TextFormats;

namespace stimulTest.ReportGenerator
{
    public abstract class BaseTextProperties<T>
    {
        protected readonly StiText CurrentText;
        protected BaseTextProperties(StiText currentText)
        {
            CurrentText = currentText;
        }
        public ITextProperties<T> WithPosition(double x = 0, double y = 0)
        {
            CurrentText.Top = y;
            CurrentText.Left = x;
            return (ITextProperties<T>) this;
        }

        public ITextProperties<T> WithSize(double width = 1, double height = 0.2)
        {
            CurrentText.Width = width;
            CurrentText.Height = height;
            return (ITextProperties<T>)this;
        }

        public ITextProperties<T> WithAlign(StiTextHorAlignment alignment = StiTextHorAlignment.Left)
        {
            CurrentText.HorAlignment = alignment;
            return (ITextProperties<T>)this;
        }
        public ITextProperties<T> WithVAlign(StiVertAlignment alignment = StiVertAlignment.Center)
        {
            CurrentText.VertAlignment = alignment;
            return (ITextProperties<T>)this;
        }
        public ITextProperties<T> WithFont(FontStyle style = FontStyle.Regular, float fontSize = 8)
        {
            CurrentText.Font = new Font(new FontFamily("Arial"), fontSize, style);
            return (ITextProperties<T>)this;
        }

        public ITextProperties<T> WithBorder(StiBorderSides sides = StiBorderSides.All)
        {
            CurrentText.Border.Side = sides;
            return (ITextProperties<T>)this;
        }

        public ITextProperties<T> WithFormat(StiFormatService format = null)
        {
            CurrentText.TextFormat = format;
            return (ITextProperties<T>)this;
        }
        public ITextProperties<T> WithWordWrap(bool state = false)
        {
            CurrentText.WordWrap = state;
            return (ITextProperties<T>)this;
        }

        public ITextProperties<T> WithDockStyle(StiDockStyle dock = StiDockStyle.None)
        {
            CurrentText.DockStyle = dock;
            return (ITextProperties<T>)this;
        }
    }
}