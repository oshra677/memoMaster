using Common1;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interface
{
    public interface IEventService
    {
        List<CBaseEvent> getAllEventToday(string id);
    }
}
