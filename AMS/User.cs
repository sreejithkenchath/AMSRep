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
    public partial class User
    {
        public User()
        {
            this.AppointmentAvails = new HashSet<AppointmentAvail>();
            this.Appointments = new HashSet<Appointment>();
            this.UserPreferences = new HashSet<UserPreference>();
        }
    
        public int UserID { get; set; }
        public int MembershipUserID { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserTitle { get; set; }
        public string UserEmail { get; set; }
        public string UserDescription { get; set; }
        public string UserPhone { get; set; }
        public Nullable<bool> UserStatus { get; set; }
        public int CompanyID { get; set; }
    
        public virtual ICollection<AppointmentAvail> AppointmentAvails { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<UserPreference> UserPreferences { get; set; }
    }
    
}
