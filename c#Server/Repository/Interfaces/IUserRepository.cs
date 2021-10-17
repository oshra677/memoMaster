
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IUserRepository
    {
        string Add_User(User u);
        void Delete_User(User u);
        void Edit_User(User u);
        string Enter_User( string id);
        int Get_Id_User_By_Mail(string gmail);
    }
}
