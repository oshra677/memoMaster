using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CategoryPreparation
    {
        public int IdCategoryPreparation { get; set; }
        public string WhatMake { get; set; }
        public string Ingredients { get; set; }
        public int? WithWhoIdPerson { get; set; }

        public BaseEvent BaseEvent { get; set; }
        public Person Person { get; set; }
    }
}
