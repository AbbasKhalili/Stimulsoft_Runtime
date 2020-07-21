using Microsoft.AspNetCore.Mvc;

namespace stimulTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentReportingController : BaseReportingController<EquipmentInspectionDto>
    {
        public EquipmentReportingController(IDataToolsService dataTools, IReportService<EquipmentInspectionDto> reportBuilder)
            : base(dataTools, reportBuilder)
        {
        }
    }
}