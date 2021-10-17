using Google.Apis.Gmail.v1.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interface
{
    public interface IManagerService
    {
        
        void ManagerText(string to, string from, DateTime date, string subjectmail, string text, string Source,string full_name);


    }
}
