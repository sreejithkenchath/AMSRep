using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using AMSDataContracts;
using AMS;
using AMS.AMSBLLFacade;
using AMSEntityAdapters;


namespace AMSEntityServices
{
    public class UserService
    {
        private IUserBLLFacade userfacade;

        public UserService()
        {
            userfacade = new AMSBLLFacade();
        }
        public UserDTOList getUsers()
        {
            List<User> users = userfacade.getUsers();
            UserDTOList ulist = new UserDTOList();
            UserAdapter ua = new UserAdapter();
            foreach (User u in users)
            {
                ulist.Add(ua.ConvertEntitytoDTO(u));
            }
            return ulist;
        }
    }
}
