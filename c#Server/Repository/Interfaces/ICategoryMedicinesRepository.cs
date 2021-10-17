using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface ICategoryMedicinesRepository
    {
        List<CategoryMedicines> GetAll();
        void Add_CategoryMedicines(CategoryMedicines w);
  
    }
}
