using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
   public class CategoryMedicinesRepository: ICategoryMedicinesRepository
    {
        private context context;

        public CategoryMedicinesRepository(context context)
        {
            this.context = context;
        }
        public int Id_Category_Medicines { get; set; }
        public string Name_Medicines { get; set; }
        public int Frequency_Week { get; set; }
        public int Frequency_Day { get; set; }
        public int Frequency_Month { get; set; }
        public DateTime Afternoon { get; set; }
        public DateTime Morning { get; set; }
        public DateTime Evening { get; set; }



        public List<CategoryMedicines> GetAll()
        {
            return context.CategoryMedicines.ToList();
        }
        public void Add_CategoryMedicines(CategoryMedicines w)
        {
            context.CategoryMedicines.Add(w);
            context.SaveChanges();
        }
    }
}
