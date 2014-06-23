using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace AMSDataContracts
{
    [DataContract()]
    public  class UserDTO
    {
        [DataMember()]
        public int UserId { get; set; }

        [DataMember()]
        public String FirstName { get; set; }

        [DataMember()]
        public String LastName { get; set; }

        [DataMember()]
        public String EmailId { get; set; }

    }

    [CollectionDataContract]
    public class UserDTOList : List<UserDTO>
    {   
    }
}
