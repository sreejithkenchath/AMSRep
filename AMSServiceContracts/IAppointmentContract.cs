using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using AMS;

namespace AMSServiceContracts
{
    public interface IAppointmentContract
    {

            [OperationContract]
        String BookAppointment(String doctorId, String userId, string from, string to);

            [OperationContract]
            List<Appointment> GetAvailableAppointments(int doctorid, DateTime ddmmyy);

            [OperationContract]
            Dictionary<int, string>  GetAvailableDoctors();
          
  
    }
}
