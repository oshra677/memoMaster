using Common1;
using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface ICalendarService
    {
        string MyINsertEvent(DateTime startdate, string subject,
            CAddress location, string mail, string language, int id,string mail2);
        void MyCalendar(DateTime date, Google.Apis.Calendar.v3.CalendarService service);
        string AddEvent(DateTime startdate, string subject,
            CAddress location, string mail, string language, int id);
        void DeleteEvent(string eventId);
    }
}
