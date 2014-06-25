using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMS.Repositories;
using AMS.Models;
using System.Collections;
using System.Globalization;

namespace AMS.AMS_BLL
{
    public class AppointmentBLL
    {
        protected IRepository DataStore { get; set; }
        protected string[] Includes { get; set; }
        AMSEntities objEntities = new AMSEntities();
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
       
         public List<DynamicAppointment> ConvertToSlots(Dictionary<String, String> Sessions, int Duration)
         {
             List<DynamicAppointment> DAppointment = new List<DynamicAppointment>();
             foreach (var x in Sessions)
             {
                 String from = x.Key;
                 String to = x.Value;
                 List<DynamicAppointment> D = new List<DynamicAppointment>();
                 D = CreateSlots(from, to, Duration);
                 DAppointment.AddRange(D);
             }
             return DAppointment;
         }
         public List<DynamicAppointment> CreateSlots(String From, String To, int Duration)
         {
             List<DynamicAppointment> DAppointments = new List<DynamicAppointment>();
             DateTime FromTime = DateTime.Parse(From);
             DateTime ToTime = DateTime.Parse(To);
             DateTime StartTime = FromTime;
             DateTime EndTime = FromTime.AddMinutes(Duration);
             while (EndTime <= ToTime)
             {
                 DynamicAppointment D = new DynamicAppointment(StartTime.ToString("hh:mm:ss tt", CultureInfo.InvariantCulture),
                     EndTime.ToString("hh:mm:ss tt", CultureInfo.InvariantCulture));
                 DAppointments.Add(D);
                 StartTime = EndTime;
                 EndTime = StartTime.AddMinutes(Duration);
             }
             return DAppointments;
         }
         public List<DynamicAppointment> GetBookedAppointments(DateTime Date, int UserID)
         {
             //Getting Booked Appointments from DB
             List<DynamicAppointment> BookedAppointments = new List<DynamicAppointment>();

             var BookedAp = (from x in objEntities.Appointments
                             where x.Date == Date && x.Status == true && x.UserID == UserID
                             select new { x.TimeFrom, x.TimeTo });
             foreach (var data in BookedAp)
             {
                 // string a =;
                 //string b = a.ToShortTimeString();

                 DynamicAppointment d = new DynamicAppointment(DateTime.Parse(Convert.ToString(data.TimeFrom)).ToLongTimeString(),
                      DateTime.Parse(Convert.ToString(data.TimeTo)).ToLongTimeString());
                 BookedAppointments.Add(d);

             }
             return BookedAppointments;

         }
         public List<DynamicAppointment> GetAppointments(int UserID, DateTime Date)
         {
             var d = (from x in objEntities.AppointmentAvails
                      join y in objEntities.DayPreferences on x.DayPrefID equals y.DayPrefID
                      join z in objEntities.TimePreferences on y.DayPrefID equals z.DayPrefID
                      join a in objEntities.UserPreferences on y.UserPrefID equals a.UserPrefID

                      where Date == x.AppDate && x.Status == true && a.UserID == UserID
                      select new { x.AppDate, z.TimeFrom, z.TimeTo, a.SlotDuration });
             int TimeSlot = 0;
             Dictionary<String, String> session = new Dictionary<String, String>();
             foreach (var data in d)
             {
                 session.Add(Convert.ToString(data.TimeFrom), Convert.ToString(data.TimeTo));
                 TimeSlot = Convert.ToInt32(data.SlotDuration);
             }
             List<DynamicAppointment> DAppointments = new List<DynamicAppointment>();
             DAppointments = ConvertToSlots(session, TimeSlot);
             //Geting booked appointments
             List<DynamicAppointment> BookedAppointmens = GetBookedAppointments(Date, UserID);
             foreach (DynamicAppointment BA in BookedAppointmens)
             {
                 foreach (DynamicAppointment DA in DAppointments)
                 {
                     TimeSpan BAFrom = TimeSpan.Parse(DateTime.Parse(BA.from).ToString("hh:mm:ss"));
                     TimeSpan DAfrom = TimeSpan.Parse(DateTime.Parse(DA.from).ToString("hh:mm:ss"));
                     TimeSpan BATo = TimeSpan.Parse(DateTime.Parse(BA.to).ToString("hh:mm:ss"));
                     TimeSpan DATo = TimeSpan.Parse(DateTime.Parse(DA.to).ToString("hh:mm:ss"));

                     //if (BA.from.Equals(DA.from) && BA.to.Equals(DA.to))
                     if (BAFrom == DAfrom && BATo == DATo)
                     {
                         DA.Status = "Booked";
                     }
                 }

             }
             return DAppointments;
         }
         public IQueryable GetCompanies()
         {
             var d = (from x in objEntities.Companies
                      where x.CompanyStatus == true
                      select new { x.CompanyName, x.CompanyID });

             return d;
         }
         public String MakeApppointment(int membershipId, DateTime D, int UserID, String TimeFrom, String Timeto)
         {
             string message;
             Appointment A = new Appointment();
             A.AppointmentNo = Guid.NewGuid().ToString();
             //A.CustID = CustomerId;
             A.Date = D;
             //A.UserID = UserID;
             Customer customer = objEntities.Customers.FirstOrDefault(e => e.MembershipUserID == membershipId);
             var Repeat = from a in objEntities.Appointments
                          where a.Status == true && a.UserID == UserID && a.CustID == customer.CustomerID && a.Date == D
                          select a;
             if (Repeat.Count() == 0)
             {


                 if (D != null && D.Date >= DateTime.Now.Date && membershipId != 0 && UserID != 0)
                 {
                     User user = objEntities.Users.FirstOrDefault(e => e.UserID == UserID);

                     DateTime test = DateTime.Parse(TimeFrom);
                     string FromDate = test.ToString("hh:mm:ss");
                     TimeSpan ts = TimeSpan.Parse(FromDate);
                     DateTime ToDate2 = DateTime.Parse(Timeto);
                     string ToDate = ToDate2.ToString("hh:mm:ss");
                     TimeSpan Te = TimeSpan.Parse(ToDate);
                     A.TimeFrom = ts;
                     A.TimeTo = Te;
                     A.User = user;
                     A.Customer = customer;
                     A.Status = true;
                     objEntities.Appointments.Add(A);
                     objEntities.SaveChanges();
                     message = "Appointment taken successfully";
                 }
                 else
                 {
                     message = "Could not make Appointment";
                     throw new Exception("Could not make Appointment");
                 }
             }
             else
             {
                 message = "Maximum number of appointment per day=1";
                 throw new Exception("Maximum number of appointment per day=1");
             }
             return message;
         }
         public IEnumerable GetUsers(int CompanyID)
         {
             Company C = objEntities.Companies.FirstOrDefault(a => a.CompanyID == CompanyID);
             var Users = from user in C.Users
                         select new { MembershipUserID = user.UserID, name = user.UserTitle + " " + user.UserFirstName + " " + user.UserLastName };
             return Users;
         }
         public List<Dictionary<int, String>> GetUsersForService(int CompanyID)
         {
             List<Dictionary<int, String>> UsersList = new List<Dictionary<int, string>>();
             Company C = objEntities.Companies.FirstOrDefault(a => a.CompanyID == CompanyID);
             var Users = from user in C.Users
                         select new { user.UserID, name = user.UserTitle + " " + user.UserFirstName + " " + user.UserLastName };
             foreach (var x in Users)
             {
                 Dictionary<int, String> D = new Dictionary<int, String>();
                 D.Add(x.UserID, x.name);
                 UsersList.Add(D);
             }
             return UsersList;
         }
         }
    }
