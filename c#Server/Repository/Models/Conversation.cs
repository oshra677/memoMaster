using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class Conversation
    {
        public Conversation()
        {
            BaseEvent = new HashSet<BaseEvent>();
        }

        public int IdConversation { get; set; }
        public int IdUser { get; set; }
        public int? IdPerson { get; set; }
        public DateTime? DateConversation { get; set; }
        public int? IdTypeConversation { get; set; }
        public string Language { get; set; }

        public Person Person { get; set; }
        public TypeConversation TypeConversation { get; set; }
        public ICollection<BaseEvent> BaseEvent { get; set; }
    }
}
