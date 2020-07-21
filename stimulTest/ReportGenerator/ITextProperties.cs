using System.Drawing;
using Stimulsoft.Base.Drawing;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.Components.TextFormats;

namespace stimulTest.ReportGenerator
{
    public interface ITextProperties<out T> : IBuilding<T>
    {
        ITextProperties<T> WithPosition(double x = 0, double y = 0);
        ITextProperties<T> WithSize(double width = 1, double height = 0.2);
        ITextProperties<T> WithAlign(StiTextHorAlignment alignment = StiTextHorAlignment.Left);
        ITextProperties<T> WithVAlign(StiVertAlignment alignment = StiVertAlignment.Center);
        ITextProperties<T> WithFont(FontStyle style = FontStyle.Regular, float fontSize = 8);
        ITextProperties<T> WithBorder(StiBorderSides sides = StiBorderSides.All);
        ITextProperties<T> WithFormat(StiFormatService format = null);
        ITextProperties<T> WithWordWrap(bool state = false);
        ITextProperties<T> WithDockStyle(StiDockStyle dock = StiDockStyle.None);
    }
}