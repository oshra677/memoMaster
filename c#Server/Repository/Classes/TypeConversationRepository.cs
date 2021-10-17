using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    class TypeConversationRepository
    {
        private context context;
        public TypeConversationRepository(context context)
        {
            this.context = context;
        }
    }
}
