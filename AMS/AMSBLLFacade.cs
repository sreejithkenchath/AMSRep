using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using AMS.AMS_BLL;
using AMS.Repositories;
using WebMatrix.WebData;

namespace AMS.AMSBLLFacade
{
    public class AMSBLLFacade : IUserBLLFacade
    {
        private UserBLL userBll;
        private AppointmentsBLL appointmentBll;

         protected IRepository DataStore { get; set; }
        protected string[] Includes { get; set; }

        public AMSBLLFacade()
        {
            userBll = new UserBLL();
            appointmentBll = new AppointmentsBLL();
            this.DataStore = new EFRepository();
        }

        #region user
        public List<User> getUsers()
        {
            return userBll.getUsers();
        }

        public string createUserPreferencesAndAppointmentSlots(UserPreference userPreference)
        {
            userBll.SetUserPreferences(userPreference);
            //appointmentBll.CreateAppointmentSlots(WebSecurity.CurrentUserId);
            return "";
        }
        
        #endregion

        #region appointments
        //code here
        public string CreateAppoinmentSlots(int userid)
        {
            return "";
        }

        #endregion

        public List<TimePreference> GetDayTimePreferences(int p, string day)
        {
            return userBll.GetDayTimePreferences(p, day);
        }

        public string setDayTimePreference(int p, string day, string firstFrom, string firstTo, string secondFrom, string secondTo)
        {
            return userBll.setDayTimePreference(p,day,firstFrom,firstTo,secondFrom,secondTo);
        }
    }
}