using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class BaseEvent
    {
        public int IdBaseEvent { get; set; }
        public int? IdConversation { get; set; }
        public DateTime? DateEvent { get; set; }
        public string TextEvent { get; set; }
        public string Subject { get; set; }
        public int? Cancel { get; set; }

        public Conversation Conversation { get; set; }
        public CategoryCommunication CategoryCommunication { get; set; }
        public CategoryEvent CategoryEvent { get; set; }
        public CategoryMedicines CategoryMedicines { get; set; }
        public CategoryMeetings CategoryMeetings { get; set; }
        public CategoryPreparation CategoryPreparation { get; set; }
        public CategoryShopping CategoryShopping { get; set; }
    }
}
