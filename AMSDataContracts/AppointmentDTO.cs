using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AMSDataContracts
{
    [DataContract()]
    public class AppointmentDTO
    {
        
            [DataMember()]
            public int AppointmentID { get; set; }

            [DataMember()]
            public String AppointmentNo { get; set; }

            [DataMember()]
            public DateTime from{ get; set; }

            [DataMember()]
            public DateTime to { get; set; }

        }

        [CollectionDataContract]
        public class AppointmentDTOList : List<AppointmentDTO>
        {
        }
    }

