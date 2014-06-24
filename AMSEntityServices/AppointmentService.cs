using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMS.AMSBLLFacade;
using AMS;

namespace AMSEntityServices
{
   public class AppointmentService
    {
         private IAppointmentBLLFacade AppFacade;

         public AppointmentService()
        {
           // AppFacade = new AMSBLLFacade();
        }
      
         public  Dictionary<int, string> GetAvailableDoctors()
             {
                 Dictionary<int, string> doctors=null;
                 return doctors;
             }
        public List<Appointment> GetAvailableAppointments(int doctorid,DateTime ddmmyy)
        {
            List<Appointment> apps=null;
            return apps;
        }
        public String BookAppointment(String doctorId, String userId,string from,string to)
        {
            return null;
        }

        }
    }

