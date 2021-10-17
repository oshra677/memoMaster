using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class CategoryShoppingRepository: ICategoryShoppingRepository
    {
        private context context;
        public CategoryShoppingRepository(context context)
        {
            this.context = context;
        }
        public int Id_Category_Shopping { get; set; }
        public string Name_Shop { get; set; }
        public bool Buy { get; set; }
        public int Money { get; set; }
        public int With_who_Id_Person { get; set; }
        public string Item_To_Bring { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string Street { get; set; }
        public string Num { get; set; }
        public string Country { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public void Add_CategoryShopping(CategoryShopping w)
        {
            context.CategoryShopping.Add(w);
            context.SaveChanges();
        }


    }
}
