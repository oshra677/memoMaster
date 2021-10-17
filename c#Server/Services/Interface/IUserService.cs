using Common1;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interface
{
   public interface IUserService
    {
        string AddUser(CUserAll cUser);
        string Enter_User(string id);
    }
}
