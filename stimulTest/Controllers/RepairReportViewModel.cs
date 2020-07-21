using System;

namespace stimulTest.Controllers
{
    public class RepairReportViewModel // rprepairQuery
    {
        public string Equipment { get; set; }
        public string Barcode { get; set; }
        public int? LastOrder { get; set; }
        public int? OrderNo { get; set; }
        public string Customer { get; set; }
        public string ProdTitle { get; set; }
        public decimal? Cost { get; set; }
        public string Location { get; set; }
        public DateTime? DateIn { get; set; }
        public DateTime? DateOut { get; set; }
        public string Notes { get; set; }
        public short? Quantity { get; set; }
        public string ResolutionDescription { get; set; }
        public string RepairReason { get; set; }
        public int? Ticket { get; set; }
        public string Department { get; set; }
        public string Category { get; set; }
        public string VendorName { get; set; }
        public string Description { get; set; }
        public string UserIn { get; set; }
        public string RepairLocation { get; set; }
    }
}