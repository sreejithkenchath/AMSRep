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
    }
}