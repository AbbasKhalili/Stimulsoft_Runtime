using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;

namespace stimulTest.Controllers
{
    public abstract class BaseReportingController<T> : Controller
    {
        private readonly IDataToolsService _dataTools;
        protected readonly IReportService<T> CurrentReportBuilder;

        protected BaseReportingController(IDataToolsService dataTools, IReportService<T> reportBuilder)
        {
            _dataTools = dataTools;
            CurrentReportBuilder = reportBuilder;
        }

        public IActionResult Index()
        {
            return View();
        }

        protected virtual async Task<StiReport> MakeReport(T model)
        {
            return await CurrentReportBuilder.GetReport(model);
        }

        
        [HttpPost, Route("loadreportpage")]
        public IActionResult LoadReportPage([FromForm]T filterQuery)
        {
            ViewBag.reportingcontroller = ControllerContext.RouteData.Values["controller"].ToString(); 
            return View("/Views/Shared/Reporting.cshtml", _dataTools.GetQueryStringOfObject(filterQuery));
        }

        [HttpPost]
        [Route("GetReport")]
        public async Task<IActionResult> GetReport([FromQuery] T model)
        {
            //var model1 = StiNetCoreViewer.GetFormValues(this);
            _dataTools.SetNullStringsEmpty(model);
            var report = await MakeReport(model);
            return await StiNetCoreViewer.GetReportResultAsync(this, report);
        }

        [Route("ViewerEvent")]
        public async Task<IActionResult> ViewerEvent()
        {
            return await StiNetCoreViewer.ViewerEventResultAsync(this);
        }
    }
}