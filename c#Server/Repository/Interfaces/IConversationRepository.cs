using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IConversationRepository
    {
       void Add_Conversation(Conversation conversation);
        void Delete_Conversation(Conversation conversation);
        void Edit_Conversation(Conversation conversation);
    }
}
