//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace AMS
{
    public partial class AppointmentAvail
    {
        public int AppAvailID { get; set; }
        public int DayPrefID { get; set; }
        public System.DateTime AppDate { get; set; }
        public Nullable<bool> Status { get; set; }
    
        public virtual DayPreference DayPreference { get; set; }
    }
    
}
