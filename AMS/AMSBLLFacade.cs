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

        public AMSBLLFacade()
        {
            user = new UserBLL();
        }

        #region user
        public List<User> getUsers()
        {
            return user.getUsers();
        }
        #endregion

        #region appointments
        //code here
        #endregion
    }
}