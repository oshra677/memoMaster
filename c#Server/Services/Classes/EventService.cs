using Common1;
using Repository;
using Repository.Classes;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Classes
{
    public class EventService:IEventService
    {
        IBaseEventRepository baseEventRepository;
        public EventService(IBaseEventRepository baseEventRepository)
        {
            this.baseEventRepository = baseEventRepository;
        }
        public List<CBaseEvent> getAllEventToday(string id)
        {
            List<CBaseEvent> cBaseEvents = new List<CBaseEvent>();
            List<Event> lr = new List<Event>();

            lr = baseEventRepository.GetAlleventToday(id);
            foreach (var item in lr)
            {
                CBaseEvent c = new CBaseEvent();
                c.address = item.address;
                c.EventTime = item.EventTime;
                c.Id = item.Id;
                c.Text = item.Text;
                c.lat = item.lat;
                c.lng = item.lng;
                cBaseEvents.Add(c);
            }
            return cBaseEvents;
        }
    } 
}
