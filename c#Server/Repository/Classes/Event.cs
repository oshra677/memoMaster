using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Classes
{
   public class Event
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime? EventTime { get; set; }
        public string address { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
    }
}
