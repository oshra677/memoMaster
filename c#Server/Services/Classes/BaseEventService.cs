using Common1;
using Repository;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class BaseEventService
    {

        public CAddress location { get; set; }
        public DateTime sdate { get; set; }
        public TimeSpan stime { get; set; }
        public string subject { get; set; }


        public int Id_BaseEvent { get; set; }
        public int Id_Conversation { get; set; }
        public DateTime Date_Event { get; set; }
        public string Text_Event { get; set; }


        public  BaseEventService(DateTime date)
        {
            sdate = date;
            Date_Event = date;
        }

    }
}
