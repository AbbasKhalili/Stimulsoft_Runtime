using System;
using System.Collections.Generic;

namespace stimulTest.Controllers
{
    public class RepairReportDto
    {
        public DateTime? Date1 { get; set; }
        public DateTime? Date2 { get; set; }
        public string Production { get; set; }
        public string RepLocationList { get; set; }
        public string Location { get; set; }
        public string Currency { get; set; }
        public string UserId { get; set; }
        public string EquipList { get; set; }
        public string Language { get; set; }
        public string OwnerList { get; set; }
        public bool IncludeBilled { get; set; }
        public bool CurrentlyInRepair { get; set; }
    }
}