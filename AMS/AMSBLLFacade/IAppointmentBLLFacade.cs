using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.AMSBLLFacade
{
     public interface IAppointmentBLLFacade
    {
        Dictionary<int,string> GetAvailableDoctors();
        List<Appointment> GetAvailableAppointments(int doctorid,DateTime ddmmyy);
        String BookAppointment(String doctorId, String userId,string from,string to);
    }
}
