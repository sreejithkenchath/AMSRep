using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMS.Repositories;
using AMS.Models;

namespace AMS.AMS_BLL
{
    public class AppointmentBLL
    {
        protected IRepository DataStore { get; set; }
        protected string[] Includes { get; set; }

         public AppointmentBLL()
        {
            //TODO: USE DEPENDENCY INJECTION FOR DECOUPLING
            this.DataStore = new EFRepository();
        }

         public List<AppointmentList> GetAppointmentDetails(int UID)
         {
             User user = DataStore.Get<User>(e => e.MembershipUserID == UID);
             List<Appointment> appointment = DataStore.Filter<Appointment>(e => e.UserID == user.UserID).ToList();
             List<AppointmentList> applist = new List<AppointmentList>();
             AppointmentList ap;
             foreach (Appointment a in appointment)
             {
                 User u = DataStore.Get<User>(b => b.UserID == a.UserID);
                 Customer c = DataStore.Get<Customer>(b => b.CustomerID == a.CustID);
                 string username = u.UserFirstName +" "+ u.UserLastName;
                 string customername = c.CustFirstName + " " + c.CustLastName;
                 ap = new AppointmentList();
                 ap.AppointmentNo = a.AppointmentNo;
                 ap.Date = a.Date.ToShortDateString();
                 ap.TimeFrom = a.TimeFrom;
                 ap.TimeTo = a.TimeTo;
                 ap.CustomerName = customername;
                 ap.UserName = username;
                 ap.Status = a.Status;
                 applist.Add(ap);
                                 
             }
             return applist;
               //  String UserName = u.UserFirstName;
               //  appointment.Add = UserName;
                 
                 
           
          }
            
         }
    }
