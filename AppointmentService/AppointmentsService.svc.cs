using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using AMSServiceContracts;
using AMSEntityServices;
using AMSDataContracts;
using AMS;



namespace AppointmenstService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class AppointmentsService : IAppointmentContract
    {
        AppointmentService us = new AppointmentService();
        public Dictionary<int, string> GetAvailableDoctors()
        {
            return us.GetAvailableDoctors();
        }


        public String BookAppointment(String doctorId, String userId,string from, string to)
        {
            return us.BookAppointment(doctorId,userId,from,to);
        }

        public List<Appointment> GetAvailableAppointments(int doctorid, DateTime ddmmyy)
        {
            return us.GetAvailableAppointments(doctorid, ddmmyy);
        }


    }
}
