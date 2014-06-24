using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMS;
using AMSDataContracts;

namespace AMSEntityAdapters
{
    public class AppointmentAdapter : IEntityAdapter<Appointment, AppointmentDTO>
    {

        public Appointment ConvertDTOtoEntity(AppointmentDTO appdto)
        {
            Appointment app = new Appointment();
            return app;
        }

        public AppointmentDTO ConvertEntitytoDTO(Appointment appointment)
        {
            AppointmentDTO appdto = new AppointmentDTO();

            
            return appdto;
        }
    }
}
