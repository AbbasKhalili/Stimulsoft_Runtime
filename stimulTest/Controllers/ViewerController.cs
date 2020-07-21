using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using Stimulsoft.Report.Web;

namespace stimulTest.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ViewerController : Controller
    {
        private readonly IReportService<EquipmentInspectionDto> _builder;

        public ViewerController(IReportService<EquipmentInspectionDto> builder)
        {
            _builder = builder;
        }

        [HttpGet]
        public IActionResult Get()
        {
            // Setting the required options on the server side
            /*var requestParams = StiNetCoreViewer.GetRequestParams(this);
            if (requestParams.Action == StiAction.Undefined)
            {
                var options = new StiNetCoreViewerOptions();
                options.Appearance.BookmarksPrint = false;

                return StiNetCoreViewer.GetScriptsResult(this, options);
            }*/

            return StiNetCoreViewer.ProcessRequestResult(this);
        }

        [HttpPost]
        public async Task<IActionResult> Post(EquipmentInspectionDto model)
        {
            var requestParams = StiNetCoreViewer.GetRequestParams(this);
            switch (requestParams.Action)
            {
                case StiAction.GetReport:
                    return await GetReportResult(model);

                default:
                    return await StiNetCoreViewer.ProcessRequestResultAsync(this);
            }
        }

        private async Task<IActionResult> GetReportResult(EquipmentInspectionDto model)
        {
            //var dataSet = new DataSet();
            //dataSet.ReadXml(StiNetCoreHelper.MapPath(this, "Reports/Data/Demo.xml"));

            //var report = new StiReport();
            //report.Load(StiNetCoreHelper.MapPath(this, "Reports/TwoSimpleLists.mrt"));
            //report.RegData(dataSet);
            var report = await _builder.GetReport(model);

            return await StiNetCoreViewer.GetReportResultAsync(this, report);
        }
    }
}