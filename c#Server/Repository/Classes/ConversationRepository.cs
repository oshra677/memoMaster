using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class ConversationRepository: IConversationRepository
    {
        private context context;
        public ConversationRepository(context context)
        {
            this.context = context;
        }
        //שאילתות
        //הוספת שיחה חדש
        public void Add_Conversation(Conversation conversation)
        {
            context.Conversation.Add(conversation);
            context.SaveChanges();
        }
        //מחיקת שיחה 
        public void Delete_Conversation(Conversation conversation)
        {
            context.Conversation.Remove(conversation);
            context.SaveChanges();
        }
        //עדכון שיחה 
        public void Edit_Conversation(Conversation conversation)
        {
            {
                Conversation c = context.Conversation.Where(l => l.IdConversation == conversation.IdConversation).FirstOrDefault();
                c.IdUser = conversation.IdUser;
                c.IdPerson = conversation.IdPerson;
                c.DateConversation = conversation.DateConversation;
                c.IdTypeConversation = conversation.IdTypeConversation;
                c.Language = conversation.Language;
                context.SaveChanges();               
            }
        }
    }
}
