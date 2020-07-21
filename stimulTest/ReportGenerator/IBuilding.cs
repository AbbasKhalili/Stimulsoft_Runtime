namespace stimulTest.ReportGenerator
{
    public interface IBuilding<out T>
    {
        T Then();
    }
}