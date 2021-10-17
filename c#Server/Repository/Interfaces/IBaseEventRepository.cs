using Repository.Classes;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IBaseEventRepository
    {
        List<BaseEvent> getallevent();
        List<Event> GetAlleventToday(string id);
        int getid();
        int SearchEvent(string subject, DateTime date);
    }
}
