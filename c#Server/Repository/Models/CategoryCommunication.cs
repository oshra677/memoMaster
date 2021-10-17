using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CategoryCommunication
    {
        public int IdCategoryCommunication { get; set; }
        public int? WhomIdPerson { get; set; }
        public string WhatToSay { get; set; }
        public int IdTypeConversation { get; set; }

        public BaseEvent BaseEvent { get; set; }
        public Person Person { get; set; }
        public TypeConversation TypeConversation { get; set; }
    }
}
