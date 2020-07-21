using System.Threading.Tasks;
using Stimulsoft.Report;
using stimulTest.ReportGenerator;

namespace stimulTest
{
    public abstract class BaseReporting<T> : IReportService<T>
    {
        protected IReportBuilder Builder;
        
        public async Task<StiReport> GetReport(T model)
        {
            return await MakeReport(model);
        }

        public abstract Task<StiReport> MakeReport(T model);
    }
}