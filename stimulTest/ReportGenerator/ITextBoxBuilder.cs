namespace stimulTest.ReportGenerator
{
    public interface ITextBoxBuilder<out T>
    {
        ITextProperties<T> New(string text = "");
    }
}