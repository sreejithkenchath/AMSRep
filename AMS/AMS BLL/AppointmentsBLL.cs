using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Antlr.Runtime;
using AMS.Repositories;
using WebMatrix.WebData;

namespace AMS.AMS_BLL
{
    public class AppointmentsBLL

    {
        protected IRepository DataStore { get; set; }
        protected string[] Includes { get; set; }

        public string CreateAppointmentSlots(int userId)
        {
            DateTime appointmentStartDate;
            DateTime appointmentEndDate;
            Includes = new[] { "UserPreferences" };
            User user = DataStore.Get<User>(e => e.MembershipUserID == WebSecurity.CurrentUserId);
            UserPreference userPreference = user.UserPreferences.FirstOrDefault();
            int availableAppointmentCount = user.AppointmentAvails.Where(e => e.Status == true).ToList().Count();
            if (availableAppointmentCount != 0)
            {
                appointmentStartDate = user.AppointmentAvails.LastOrDefault().AppDate.AddDays(1);
                appointmentEndDate = appointmentStartDate.AddDays(userPreference.BookingDays - availableAppointmentCount);
            }
            else
            {
                appointmentStartDate = DateTime.Now;
                appointmentEndDate = appointmentStartDate.AddDays(userPreference.BookingDays);
            }
    
            for (DateTime date = appointmentStartDate; date <= appointmentEndDate; date=date.AddDays(1))
            {
                foreach (DayPreference dayPreference in userPreference.DayPreferences)
                {
                    if ((int) date.DayOfWeek == Convert.ToInt32(dayPreference.Day))
                    {
                        AppointmentAvail appointmentAvail = new AppointmentAvail();
                        appointmentAvail.AppDate = date;
                        appointmentAvail.DayPreference = dayPreference;
                        appointmentAvail.User = user;
                        user.AppointmentAvails.Add(appointmentAvail);
                        DataStore.Create<AppointmentAvail>(appointmentAvail);
                        break;
       
                    }
                }
                
            }

            return "";
        } 
    }
}