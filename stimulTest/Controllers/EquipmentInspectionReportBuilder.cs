using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using stimulTest.ReportGenerator;

namespace stimulTest.Controllers
{
    public class EquipmentInspectionReportBuilder : BaseReporting<EquipmentInspectionDto>
    {
        private readonly IEquipmentInspectionRepository _repository;

        public EquipmentInspectionReportBuilder(IEquipmentInspectionRepository repository) 
        {
            _repository = repository;
        }

        public override async Task<StiReport> MakeReport(EquipmentInspectionDto model)
        {
            model.Date1 = new DateTime(2020, 1, 1);
            model.Date2 = new DateTime(2020, 6, 1);
            model.UserId = "rti";

            var data = await _repository.GetReport(model);

            Builder = new ReportBuilder(StiPageOrientation.Portrait);

            var props = GetConditions(model);
            var groupTitle = GetGroupTitles();
            var sizes = GetSizes(model.SetupMultiLocation);
            var titles = GetTitles(model.SetupMultiLocation);
            var fields = GetFields(model.SetupMultiLocation);

            var report = Builder.WithDataSource(data)
                .WithPageHeaderBand("Rental Tracker Pro", "Equipment Inspection", 0.5)
                .WithReportTitleBand(props)
                .WithGroupHeaderBand("{dataset.Barcode}", groupTitle, 0.3)
                .WithHeaderBand(titles, sizes)
                .WithDataBand(fields, sizes)
                .Build();

            return report;
        }

        private Dictionary<string, string> GetConditions(EquipmentInspectionDto model)
        {
            var props = new Dictionary<string, string>
            {
                {"Date In Inspection Range", $"{model.Date1.ToShortDateString()} - {model.Date2.ToShortDateString()}"},
                {"Barcode Range", $"{model.Barcode1} - {model.Barcode2}"},
                {"Inspection Type", model.InspectionType},
                {"Location", model.Location},
            };
            if (!string.IsNullOrEmpty(model.DepartmentList)) props.Add("Department", "Multiple");
            if (!string.IsNullOrEmpty(model.CategoryList)) props.Add("Category", "Multiple");
            if (!string.IsNullOrEmpty(model.EquipList)) props.Add("Equipment", "Multiple");
            if (!string.IsNullOrEmpty(model.Equip1) && !string.IsNullOrEmpty(model.Equip2))
                props.Add("Equipment Range", model.Equip1 + "-" + model.Equip2);

            return props;
        }

        private List<string> GetGroupTitles()
        {
            return new List<string>
            {
                "Barcode: {dataset.Barcode}",
                "SerialNumber: {dataset.SerialNumber}",
                "Equipment: {dataset.Equipment}"
            };
        }

        private static double[] GetSizes(bool multiLocation)
        {
            var sizes = new[] { 0.7, 0.9, 0.8, 0.8, 0.8, 2.7, 0.8 };
            if (multiLocation)
                sizes = new[] { 0.6, 0.8, 0.7, 0.7, 0.7, 2.6, 0.7, 0.7 };
            return sizes;
        }

        private static List<string> GetTitles(bool multiLocation)
        {
            var titles = new List<string>
            {
                "Ticket" ,
                "Inspection~Type" ,
                "Date In~Inspection" ,
                "Date Out of~Inspection" ,
                "Inspected~by" ,
                "Result"
            };
            if (multiLocation)
                titles.Add("Location");
            titles.Add("User~In");
            return titles;
        }

        private static List<string> GetFields(bool multiLocation)
        {
            var fields = new List<string>
            {
                "Ticket",
                "InspectionType",
                "DateIn",
                "DateOut",
                "InspectedBy",
                "Result"
            };
            if (multiLocation)
                fields.Add("Location");
            fields.Add("UserIn");
            return fields;
        }


    }
}