using System;
using System.Collections.Generic;
using System.Text;

namespace Common1
{
    public class CBaseEvent
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime? EventTime { get; set; }
        public string address { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
    }
}
