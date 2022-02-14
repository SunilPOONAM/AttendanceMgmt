using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendenceMgmt.Models
{
    public class DailyAtt
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string TimeIn { get; set; }
        public string TimeInImage { get; set; }
        public string TimeOut { get; set; }
        public string TimeOutImage { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string Accuracy { get; set; }
        public string Date { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}