using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMS.AMS_BLL;

namespace AMS.AMSBLLFacade
{
    public class AMSBLLFacade : IUserBLLFacade
    {
        UserBLL user;
     ////   AppointmentBLL appointment;

     //   public AMSBLLFacade()
     //   {
     //       user = new UserBLL();
     //       appointment = new AppointmentBLL();
     //   }

        #region user
        public List<User> getUsers()
        {
            return user.getUsers();
        }
        #endregion

        //#region appointments
        //Dictionary<int, string> GetAvailableDoctors()
        //{

        //}

        //List<Appointment> GetAvailableAppointments(int doctorid, DateTime ddmmyy)
        //{
        //}
        //String BookAppointment(String doctorId, String userId, string from, string to)
        //{
        //}
        //#endregion
        AppointmentsBLL appBll;

        public AMSBLLFacade()
        {
            user = new UserBLL();
            appBll = new AppointmentsBLL();
        }

        public string createUserPreference(UserPreference userpreference)
        {
            return user.SetUserPreferences(userpreference);
            //appBll.CreateAppointmentSlots
        }

        public List<TimePreference> GetDayTimePreferences(int p, string day)
        {
            return user.GetDayTimePreferences(p, day);
        }

        public string setDayTimePreference(int p, string day, string firstFrom, string firstTo, string secondFrom, string secondTo)
        {
           return user.setDayTimePreference(p, day, firstFrom, firstTo, secondFrom, secondTo);
        }

        public string CreateSlots(Models.AppointmentSlots slot, int userId)
        {
            appBll.CreateAppointmentSlots(slot.numberOfdays, userId);
        }
    }
}