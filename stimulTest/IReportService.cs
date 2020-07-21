using System.Threading.Tasks;
using Stimulsoft.Report;

namespace stimulTest
{
    public interface IReportService<in T>
    {
        Task<StiReport> GetReport(T model);
    }
}
