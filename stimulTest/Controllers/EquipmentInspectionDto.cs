using System;

namespace stimulTest.Controllers
{
    public class EquipmentInspectionDto
    {
        public string DepartmentList { get; set; }
        public string CategoryList { get; set; }
        public string Equip1 { get; set; }
        public string Equip2 { get; set; }
        public string Barcode1 { get; set; }
        public string Barcode2 { get; set; }
        public DateTime Date1 { get; set; }
        public DateTime Date2 { get; set; }
        public string InspectionType { get; set; }
        public string Location { get; set; }
        public string UserId { get; set; }
        public string EquipList { get; set; }
        public string Language { get; set; }
        public bool SetupMultiLocation { get; set; }
    }
}