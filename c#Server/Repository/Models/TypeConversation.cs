using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class TypeConversation
    {
        public TypeConversation()
        {
            CategoryCommunication = new HashSet<CategoryCommunication>();
            Conversation = new HashSet<Conversation>();
        }

        public int IdTypeConversation { get; set; }
        public string Description { get; set; }

        public ICollection<CategoryCommunication> CategoryCommunication { get; set; }
        public ICollection<Conversation> Conversation { get; set; }
    }
}
