using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using AMS.Repositories;
using WebMatrix.WebData;
using System.Web.Security;
using System.Transactions;

namespace AMS.AMS_BLL
{
    public class UserBLL
    {
        private string Message;
        private bool IsValid;
        protected IRepository DataStore { get; set; }
        protected string[] Includes { get; set; }

        public UserBLL()
        {
            //TODO: USE DEPENDENCY INJECTION FOR DECOUPLING
            this.DataStore = new EFRepository();
        }

        public string AddNewUser(FormCollection collection, User user)
        {
            Message = null;
            IsValid = true;
            AMSEntities ae = new AMSEntities();
            // User emailcheck = ae.Users.Where(e => e.UserEmail == user.UserEmail).Single();

            //   if (emailcheck != null)
            //       SetError("Email Id already Exists");

            //User uu=ae.Users.Where(e => e.MembershipUserID == WebSecurity.CurrentUserId).Single();
            User uu = DataStore.Get<User>(e => e.MembershipUserID == WebSecurity.CurrentUserId);
            String uname = collection.Get("UserName");
            String paswd = collection.Get("Password");
            user.CompanyID = uu.CompanyID;
            if (IsValid)
            {
                using (TransactionScope t = new TransactionScope())
                {
                    WebSecurity.CreateUserAndAccount(uname, paswd);
                    Roles.AddUserToRole(uname, "User");
                    user.MembershipUserID = WebSecurity.GetUserId(uname);
                    //ae.Users.AddObject(user);
                    //ae.SaveChanges();
                    DataStore.Create(user);
                    DataStore.SaveChanges();
                    Emailer.Send(user.UserEmail, "Welcome to AMS", "Please reset your password");
                    t.Complete();
                };
                Message = "successfully added a new user";
                return Message;
            }
            else
            {
                return Message;
            }
        }

        public void SetError(string message)
        {
            IsValid = false;
            Message += message;
        }

        public List<User> GetUsersForCompany(int p)
        {
            return DataStore.Filter<User>(e => e.CompanyID == p).ToList();
        }

        public User GetUserbyId(int p)
        {
            return DataStore.Get<User>(e => e.MembershipUserID == p);
        }

        public User GetUserDetails(int uid)
        {
            User user = DataStore.Get<User>(e => e.MembershipUserID == uid);
            return user;
        }

        public List<User> getUsers()
        {
            return DataStore.All<User>().ToList();
        }

        public string SetUserPreferences(UserPreference userPreference)
        {
            Includes = new[] { "UserPreferences" };
            string dayTimeValidationMessage = "User Preferences set succesfully";
            User thisUser = DataStore.Get<User>(e=>e.MembershipUserID == WebSecurity.CurrentUserId,Includes);
            if (thisUser.UserPreferences.Count == 1)
            {
                UserPreference tempUserPreference = DataStore.Get<UserPreference>(e => e.UserID == thisUser.UserID);
                tempUserPreference.BookingDays = userPreference.BookingDays;
                tempUserPreference.SlotDuration = userPreference.SlotDuration;
                DataStore.SaveChanges();
            }
            else if (thisUser.UserPreferences.Count > 1)
            {
                DataStore.Delete<UserPreference>(e => e.UserID == thisUser.UserID);
            }
            if (thisUser.UserPreferences.Count == 0)
            {
                userPreference.User = thisUser;
                DataStore.Create<UserPreference>(userPreference);
            }

        
            return dayTimeValidationMessage;
        }

        public bool validateUserPreferences(UserPreference userPreference, out string message)
        {
            message = string.Empty;
            foreach (DayPreference day in userPreference.DayPreferences)
            {
                if (day == null || Convert.ToInt32(day.Day) < 1 || Convert.ToInt32(day.Day) > 7)
                {
                    message = "Invalid day";
                    return false;
                }
                else
                {
                    foreach (TimePreference timePreference in day.TimePreferences)
                    {
                        if (timePreference.TimeTo <= timePreference.TimeFrom)
                        {
                            message = "From  Time must be lesser than  To time";
                            return false;
                        }
                        else
                        {
                            foreach (var tempTimePref in day.TimePreferences)
                            {
                                if (tempTimePref.TimeFrom <= timePreference.TimeFrom
                                    && tempTimePref.TimeTo > timePreference.TimeFrom
                                    && tempTimePref.TimeTo <= timePreference.TimeTo)
                                {
                                    message = "Time spans cannot intersect";
                                    return false;
                                }
                                else if (tempTimePref.TimeFrom >= timePreference.TimeFrom
                                         && tempTimePref.TimeFrom < timePreference.TimeTo
                                         && tempTimePref.TimeTo >= timePreference.TimeTo)
                                {
                                    message = "Time spans cannot intersect";
                                    return false;
                                }
                                else if (tempTimePref.TimeFrom <= timePreference.TimeFrom
                                         && tempTimePref.TimeTo >= timePreference.TimeTo)
                                {
                                    message = "Time spans cannot intersect";
                                    return false;
                                }
                                else if (tempTimePref.TimeFrom <= timePreference.TimeFrom
                                         && tempTimePref.TimeTo <= timePreference.TimeTo)
                                {
                                    message = "Time spans cannot intersect";
                                    return false;
                                }
                            }
                        }
                    }


                }
            }
            return true;
        }

        public List<TimePreference> GetDayTimePreferences(int p, string day)
        {
           // Includes = new[] { "UserPreferences" };
            UserPreference up=null;
            DayPreference dp = null;
            string dayOfTheWeek = DayOfTheWeek(day).ToString();
            User user = DataStore.Get<User>(e => e.MembershipUserID == p);
            if (user != null)
            {
                up = DataStore.Get<UserPreference>(e => e.UserID == user.UserID);
                Includes = new[] {"TimePreferences"};
            }
            if (up != null)
            {
                dp = DataStore.Get<DayPreference>(e => e.UserPrefID == up.UserPrefID && e.Day.Equals(dayOfTheWeek),Includes);
            }
            return dp.TimePreferences.ToList();
        }

        public string setDayTimePreference(int p, string day, string firstFrom, string firstTo, string secondFrom, string secondTo)
        {
            Includes = new[] { "UserPreferences" };
            string message = "Day Time preference set successfully";
            User user = DataStore.Get<User>(e => e.MembershipUserID == p,Includes);
            string dayOfTheWeek = this.DayOfTheWeek(day).ToString();
            DayPreference dp;
            IList<TimePreference> timePreferences;
            if (user.UserPreferences.Count != 0)
            {
                UserPreference userPreference = DataStore.Get<UserPreference>(e => e.UserID == user.UserID);
                if (timeValidate(firstFrom, firstTo, secondFrom, secondTo))
                {
                    DataStore.Delete<DayPreference>(
                        e => e.UserPrefID == userPreference.UserPrefID && e.Day == dayOfTheWeek);
                    dp = new DayPreference();
                    dp.Day = dayOfTheWeek;
                    dp.UserPreference = userPreference;
                    DataStore.Create<DayPreference>(dp);
                    TimePreference tp1 = new TimePreference();
                    TimePreference tp2 = new TimePreference();
                    TimeSpan temp;
                    TimeSpan.TryParse(firstFrom, out temp);
                    tp1.TimeFrom = temp;
                    TimeSpan.TryParse(firstTo, out temp);
                    tp1.TimeTo = temp;
                    TimeSpan.TryParse(secondFrom, out temp);
                    tp2.TimeFrom = temp;
                    TimeSpan.TryParse(secondTo, out temp);
                    tp2.TimeTo = temp;
                    tp1.DayPreference = dp;
                    tp2.DayPreference = dp;
                    DataStore.Create<TimePreference>(tp1);
                    DataStore.Create<TimePreference>(tp2);
                }
                else
                {
                    message = "Invalid time";
                }
            }
            else
            {
                message = "Set User Preference First";
            }
            return message;
        }

        private bool timeValidate(string firstFrom, string firstTo, string secondFrom, string secondTo)
        {
            bool isValid = true;
            TimeSpan t11;
            TimeSpan t12;
            TimeSpan t21;
            TimeSpan t22;
            isValid = TimeSpan.TryParse(firstFrom, out t11);
            if (isValid)
            {
                isValid = TimeSpan.TryParse(firstTo, out t12);
                if (isValid)
                {
                    isValid = TimeSpan.TryParse(secondFrom, out t21);
                    if (isValid)
                    {
                        isValid = TimeSpan.TryParse(secondTo, out t22);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
            if (t12 <= t11 || t22 <= t21)
            {
                return false;
            }
            else
            {
                if (t11 <= t21 && t12 > t21 && t12 <= t22)
                {
                    return false;
                }
                else if (t11 >= t21
                         && t11 < t22 && t12 >= t22)
                {
                    return false;
                }
                else if (t11 <= t21 && t12 >= t22)
                {
                    return false;
                }
                else if (t11 >= t21 && t12 <= t22)
                {
                    return false;
                }
            }
            return true;
        }

        public int DayOfTheWeek(string day)
        {
            int dayOfTheWeek = 0;
            if (day.Equals("Sunday"))
            {
                dayOfTheWeek = 1;
            }
            else if (day.Equals("Monday"))
            {
                dayOfTheWeek = 2;
            }
            else if (day.Equals("Tuesday"))
            {
                dayOfTheWeek = 3;
            }
            else if (day.Equals("Wednesday"))
            {
                dayOfTheWeek = 4;
            }
            else if (day.Equals("Thursday"))
            {
                dayOfTheWeek = 5;
            }
            else if (day.Equals("Friday"))
            {
                dayOfTheWeek = 6;
            }
            else if (day.Equals("Saturday"))
            {
                dayOfTheWeek = 7;
            }
            return dayOfTheWeek;
        }


    }
}