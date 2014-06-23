using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMSDataContracts;
using AMS;

namespace AMSEntityAdapters
{
    public class UserAdapter : IEntityAdapter<User, UserDTO>
    {
        public User ConvertDTOtoEntity(UserDTO userdto)
        {
            User user = new User();
            return user;
        }

        public UserDTO ConvertEntitytoDTO(User user)
        {
            UserDTO userdto = new UserDTO();
            userdto.UserId = user.MembershipUserID;
            userdto.FirstName = user.UserFirstName;
            userdto.LastName = user.UserLastName;
            userdto.EmailId = user.UserEmail;   
                return userdto;
        }



    }
}
