using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private context context;
        public UserRepository(context context)
        {
            this.context = context;
        }
        //הוספת משתמש חדש
        public string Add_User(User u)
        {
            if (context.User.Any(l => l.Id == u.Id))
                return "-1";
            else

            {
                context.User.Add(u);
                context.SaveChanges();
                return u.Id;
            }
            
        }
        //מחיקת משתמש 
        public void Delete_User(User u)
        {
            context.User.Remove(u);
            context.SaveChanges();
        }
        //עדכון משתמש 
        public void Edit_User(User u)
        {

            User user_edit = context.User.Where(l => l.IdUser == u.IdUser).FirstOrDefault();
            user_edit.FirstName = u.FirstName;
            user_edit.LastName = u.LastName;
            user_edit.Telephon = u.Telephon;
            user_edit.Pelephon = u.Pelephon;
            user_edit.Email = u.Email;
            user_edit.City = u.City;
            user_edit.Neighborhood = u.Neighborhood;
            user_edit.Street = u.Street;
            user_edit.Country = u.Country;
            user_edit.Num = u.Num;
            user_edit.Id = u.Id;
            user_edit.Sex = u.Sex;
            user_edit.BirthDate = u.BirthDate;
            context.SaveChanges();

        }
        //הכנסת משתמש
        public string Enter_User(string id)
        {
            string s;
            if (!context.User.Any(l => l.Id == id))
            {
                s = "0";
                return s;
            }
            return id;
        }
        //פונקציה שמחזירה מזהה משתמש לפי כתובת מייל שמתקבלת
        public int Get_Id_User_By_Mail(string gmail)
        {
            if (context.User.Any(l => l.Email == gmail))
            {
                foreach (var item in gmail)
                {
                    if (item == '@')
                        return context.User.FirstOrDefault(l => l.Email == gmail).IdUser;
                }

            }
                return -1;
        }
    }
}

