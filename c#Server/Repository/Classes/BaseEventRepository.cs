using Microsoft.EntityFrameworkCore;
using Repository.Classes;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public  class BaseEventRepository: IBaseEventRepository
    {
        private context context;
        

        public BaseEventRepository(context context)
        {
            this.context = context; 
        }
        public int Id_BaseEvent { get; set; }
        public int Id_Conversation { get; set; }
        public DateTime Date_Event { get; set; }
        public string Text_Event { get; set; }
        public string Subject { get; set; }

       
     public int SearchEvent(string subject, DateTime date)
        {
            context.BaseEvent.Where(c => c.Subject == subject && c.DateEvent == date).First().Cancel = 1;
            return context.BaseEvent.Where(l => l.Subject == subject && l.DateEvent == date).First().IdBaseEvent;
        }

        //פונקציה שמחזירה את כל האירועים
        public List<BaseEvent> getallevent()
        {
            return context.BaseEvent.ToList();
        }
        public  List<Event> GetAlleventToday(string id)
        {
            List<Event> allList = new List<Event>();
            var listEvent = from u in context.BaseEvent
                       join ev in context.Conversation on u.IdConversation
                       equals ev.IdConversation
                       join w in context.User on ev.IdUser equals w.IdUser
                       where u.Cancel!=1&&w.Id == id && u.DateEvent.Value.Date == DateTime.Now.Date&& u.DateEvent.Value>=DateTime.Now&&u.Subject=="Event"
                       select new Event
                       {
                           
                           EventTime = u.DateEvent,
                           Id = u.IdBaseEvent,
                           Text = u.TextEvent,
                           address = u.CategoryEvent.Country.ToString()+" "+u.CategoryEvent.City.ToString()+" "+u.CategoryEvent.Neighborhood.ToString()+ " "+u.CategoryEvent.Street.ToString() + " " + u.CategoryEvent.Num.ToString() + " ",
                           lat = u.CategoryEvent.Lat,
                           lng = u.CategoryEvent.Lng
                       };
            
            foreach (var item in listEvent)
            {
                allList.Add(item);
            }
            var listMeetings = from u in context.BaseEvent
                       join ev in context.Conversation on u.IdConversation
                       equals ev.IdConversation
                       join w in context.User on ev.IdUser equals w.IdUser
                       where u.Cancel != 1 && w.Id == id && u.DateEvent.Value.Date == DateTime.Now.Date&& u.DateEvent.Value >= DateTime.Now && u.Subject == "Meetings"
                       select new Event
                       {
                           EventTime = u.DateEvent,
                           Id = u.IdBaseEvent,
                           Text = u.TextEvent,
                          address=u.CategoryMeetings.Country.ToString()+" "+ u.CategoryMeetings.City.ToString()+" "+ u.CategoryMeetings.Neighborhood.ToString()+" " + u.CategoryMeetings.Street.ToString()+" " + u.CategoryMeetings.Num.ToString()+" ",
                           lat = u.CategoryMeetings.Lat,
                           lng = u.CategoryMeetings.Lng
                       };
            foreach (var item in listMeetings)
            {
                allList.Add(item);
            }
            var listShopping = from u in context.BaseEvent
                       join ev in context.Conversation on u.IdConversation
                       equals ev.IdConversation
                       join w in context.User on ev.IdUser equals w.IdUser
                       where u.Cancel != 1 &&  w.Id == id && u.DateEvent.Value.Date == DateTime.Now.Date && u.DateEvent.Value >= DateTime.Now && u.Subject == "Shopping"
                       select new Event
                       {
                           EventTime = u.DateEvent,
                           Id = u.IdBaseEvent,
                           Text = u.TextEvent,
                           address = u.CategoryShopping.Country.ToString() + " " + u.CategoryShopping.City.ToString() + " " + u.CategoryShopping.Neighborhood.ToString() + " " + u.CategoryShopping.Street.ToString() + " " + u.CategoryShopping.Num.ToString() + " ",
                           lat = u.CategoryShopping.Lat,
                           lng = u.CategoryShopping.Lng
                       };
            foreach (var item in listShopping)
            {
                allList.Add(item);
            }

            return allList.ToList();
        }

        //פונקציה שמחזירה את האי די של האירוע האחרון
         public int getid()
        {

            return context.BaseEvent.Last().IdBaseEvent;
        }
    }
}
