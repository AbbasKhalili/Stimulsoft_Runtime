using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stimulsoft.Report;
using Stimulsoft.Report.Engine;

namespace stimulTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairReportController : ControllerBase
    {
        private readonly IDataToolsService _dataTools;
        private readonly IReportService<RepairReportDto> _reportBuilder;
        private readonly IRepairReportRepository _repository;

        public RepairReportController(IDataToolsService dataTools, IReportService<RepairReportDto> reportBuilder,
            IRepairReportRepository repository)
        {
            _dataTools = dataTools;
            _reportBuilder = reportBuilder;
            _repository = repository;
        }


        //ReportStyle<List<RepairReportViewModel>>
        [HttpPost("makereport")]
        public async Task<string> MakeReport([FromBody] RepairReportDto model)
        {
            _dataTools.SetNullStringsEmpty(model);

            var report = await _reportBuilder.GetReport(model);
            //report.CalculationMode = StiCalculationMode.Interpretation;
            report.Save("C:\\Csharp-Projects\\stimulTest\\stimulTest\\rep.mrt");
            report.Render(false);
            report.ExportDocument(StiExportFormat.Pdf, "C:\\Csharp-Projects\\stimulTest\\stimulTest\\rep.pdf");

            var result = report.SaveDocumentJsonToString();



            //var viewmodel = await _repository.GetReport(model);

            //var result = new ReportStyle<List<RepairReportViewModel>>
            //{
            //    ReportTemplate = report.SaveToString(),
            //    Data = new Pagination<List<RepairReportViewModel>>()
            //    {
            //        Count = 30,
            //        Page = viewmodel.Count / 30,
            //        Total = viewmodel.Count,
            //        DataSets = new List<DataSet<List<RepairReportViewModel>>>
            //        {
            //            new DataSet<List<RepairReportViewModel>>()
            //            {
            //                DataSetName = "dataset",
            //                Resource = viewmodel
            //            }
            //        }
            //    }
            //};
            return result;
        }
    }

    public class ReportStyle<T>
    {
        public Pagination<T> Data { get; set; }
        public string ReportTemplate { get; set; }
    }

    public class Pagination<T>
    {
        public int Page { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }
        public List<DataSet<T>> DataSets { get; set; }
    }

    public class DataSet<T>
    {
        public string DataSetName { get; set; }
        public T Resource { get; set; }
    }
}