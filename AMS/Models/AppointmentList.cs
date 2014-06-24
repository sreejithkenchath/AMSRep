using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models
{
    public class AppointmentList
    {

    
        public int AppointmentNo { get; set; }
        public string Date { get; set; }
        public System.TimeSpan TimeFrom { get; set; }
        public System.TimeSpan TimeTo { get; set; }
        public string UserName { get; set; }
        public string CustomerName { get; set; }
        public Nullable<bool> Status { get; set; }

    }
}