using System;
using System.Collections.Generic;
using System.Text;

namespace Common1
{
    public abstract class CBaseEvent
    {
        public int Id { get; set; }
        public int id_base_event { get; set; }
        public string Text { get; set; }
        public DateTime? EventTime { get; set; }
    }

    public class MedicineEvent : CBaseEvent
    {
        public string Name { get; set; }
    }
}
