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

        public AppointmentsBLL()
        {
            DataStore = new EFRepository();
        }

        public string CreateAppointmentSlots(int numberOf, int userId)
        {
            DateTime appointmentStartDate;
            DateTime appointmentEndDate;
            Includes = new[] { "UserPreferences", "AppointmentAvails" };
            User user = DataStore.Get<User>(e => e.MembershipUserID == userId,Includes);
            UserPreference tempUserPreference = user.UserPreferences.FirstOrDefault();
            Includes = new[] { "DayPreferences"};
            UserPreference userPreference = DataStore.Get<UserPreference>(e => e.UserPrefID == tempUserPreference.UserPrefID,Includes);
            int availableAppointmentCount = user.AppointmentAvails.ToList().Count();
            if (userPreference != null)
            {
                if (availableAppointmentCount != 0)
                {
                    appointmentStartDate = user.AppointmentAvails.LastOrDefault().AppDate.AddDays(1);
                    appointmentEndDate = appointmentStartDate.AddDays(numberOf);
                }
                else
                {
                    appointmentStartDate = DateTime.Now;
                    appointmentEndDate = appointmentStartDate.AddDays(numberOf);
                }
                for (DateTime date = appointmentStartDate; date <= appointmentEndDate; date = date.AddDays(1))
                {
                    foreach (DayPreference dayPreference in userPreference.DayPreferences)
                    {
                        if ((int)date.DayOfWeek == Convert.ToInt32(dayPreference.Day))
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
                return "slots created successfully";
            }
            return "Could not create";
        } 
    }
}