using Microsoft.AspNetCore.Mvc;

namespace stimulTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairReportingController : BaseReportingController<RepairReportDto>
    {
        public RepairReportingController(IDataToolsService dataTools, IReportService<RepairReportDto> reportBuilder)
            : base (dataTools, reportBuilder)
        {
        }
    }
}