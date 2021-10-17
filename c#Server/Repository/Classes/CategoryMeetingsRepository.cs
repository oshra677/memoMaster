using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class CategoryMeetingsRepository: ICategoryMeetingsRepository
    {

        private context context;
        public CategoryMeetingsRepository(context context)
        {
            this.context = context;
        }

        public int Id_Category_Meetings { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public int With_who_Id_Person { get; set; }
        public int Whose_Id_Person { get; set; }
        public string Item_To_Bring { get; set; }
        public string Street { get; set; }
        public string Num { get; set; }
        public string Country { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }

        public void Add_CategoryMeetings(CategoryMeetings w)
        {
            context.CategoryMeetings.Add(w);
            context.SaveChanges();
        }
       
    }
}
