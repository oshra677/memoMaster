using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class CategoryPreparationRepository: ICategoryPreparationRepository
    {
        private context context;
        public CategoryPreparationRepository(context context)
        {
            this.context = context;
        }
        public int Id_Category_Preparation { get; set; }
        public string What_Make { get; set; }
        public string Ingredients { get; set; }
        public int With_who_Id_Person { get; set; }
        public void Add_CategoryPreparation(CategoryPreparation w)
        {
            context.CategoryPreparation.Add(w);
            context.SaveChanges();
        }

    }
}
