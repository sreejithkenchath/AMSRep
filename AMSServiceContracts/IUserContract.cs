using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using AMSDataContracts;

namespace AMSServiceContracts
{
    [ServiceContract()]
    public interface IUserContract
    {
        [OperationContract]
        UserDTOList getUsers();
    }
}
