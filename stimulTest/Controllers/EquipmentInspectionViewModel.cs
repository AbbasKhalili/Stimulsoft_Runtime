using System;

namespace stimulTest.Controllers
{
    public class EquipmentInspectionViewModel
    {
        public int? Ticket { get; set; }
        public string InspectionType { get; set; }
        public DateTime? DateIn { get; set; }
        public DateTime? DateOut { get; set; }
        public string UserIn { get; set; }
        public string Result { get; set; }
        public string InspectedBy { get; set; }
        public string Location { get; set; }
        public string InspectionNotes { get; set; }
        public string ResolutionNotes { get; set; }
        public string InspectionTypeNotes { get; set; }
        public string Barcode { get; set; }
        public string SerialNumber { get; set; }
        public string Equipment { get; set; }
        public string EquipmentDescription { get; set; }
    }
}