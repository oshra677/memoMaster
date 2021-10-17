using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class CategoryCommunicationRepository: ICategoryCommunicationRepository
    {
        private context context;
        public CategoryCommunicationRepository(context context)
        {
            this.context = context;
        }
        public int Id_Category_Communication { get; set; }
        public int Whom_Id_Person { get; set; }
        public string What_to_say { get; set; }
        public int Id_Type_Conversation { get; set; }
        public void Add_Communication(CategoryCommunication w)
        {
            context.CategoryCommunication.Add(w);
            context.SaveChanges();
        }

    }
}
