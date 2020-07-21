using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using stimulTest.Controllers;
using stimulTest.ReportGenerator;

namespace stimulTest.Builder
{
    public class RepairReportBuilder : BaseReporting<RepairReportDto>
    {
        private readonly IRepairReportRepository _repository;
        
        public RepairReportBuilder(IRepairReportRepository repository) : base ()
        {
            _repository = repository;
        }

        public override async Task<StiReport> MakeReport(RepairReportDto model)
        {
            var data = await _repository.GetReport(model);

            Builder = new ReportBuilder(StiPageOrientation.Landscape);

            var props = GetConditions(model);
            var sizes = GetSizes();
            var titles = GetTitles();
            var fields = GetFields();

            var report = Builder.WithDataSource(data)
                .WithPageHeaderBand("Rental Tracker", "Repair Report", 0.5)
                .WithReportTitleBand(props)
                .WithHeaderBand(titles, sizes)
                .WithDataBand(fields, sizes)
                .WithFooterBand(new List<string> { "Subtotal", "${Sum(dataset.Cost)}" }, new[] { 10.2, 0.7 })
                .Build();

            return report;
        }

        private Dictionary<string, string> GetConditions(RepairReportDto model)
        {
            var props = new Dictionary<string, string>
            {
                {"Date Range", $"{model.Date1?.ToShortDateString()} - {model.Date2?.ToShortDateString()}"},
                {"Location", model.Location},
                {"Currency", model.Currency}
            };
            if (model.IncludeBilled) props.Add("Include Billed", model.IncludeBilled.ToString());
            if (model.CurrentlyInRepair) props.Add("Currently In Repair", model.CurrentlyInRepair.ToString());

            return props;
        }
        
        private static double[] GetSizes()
        {
            return new[] { 0.8, 2.6, 0.8, 0.3, 0.5, 0.8, 0.7, 0.7, 0.6, 0.6, 1.0, 0.8, 0.7 };
        }

        private static List<string> GetTitles()
        {
            return new List<string>
            {
                "Equipment",
                "Description",
                "Barcode#",
                "Qty",
                "Order#",
                "Production",
                "User~In",
                "Date~In",
                "Date~Out",
                "Ticket",
                "Resolution~Description",
                "Repair~Vendor",
                "Repair~Cost"
            };
        }

        private static List<string> GetFields()
        {
            return new List<string>
            {
                "Equipment",
                "Description",
                "Barcode",
                "Quantity",
                "OrderNo",
                "Customer",
                "UserIn",
                "DateIn",
                "DateOut",
                "Ticket",
                "ResolutionDescription",
                "RepairLocation",
                "$Cost"
            };
        }
    }
}
