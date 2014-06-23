using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.AMSBLLFacade
{
    public interface IUserBLLFacade
    {
        List<User> getUsers();
    }
}