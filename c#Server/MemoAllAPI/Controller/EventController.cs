
using Common1;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemoAllAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        IEventService eventService;
        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        [HttpGet("EventDay")]

        public List<CBaseEvent> Get(string id)
        {
            return eventService.getAllEventToday(id);
        }

    }


}