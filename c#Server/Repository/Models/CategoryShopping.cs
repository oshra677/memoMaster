using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CategoryShopping
    {
        public int IdCategoryShopping { get; set; }
        public string NameShop { get; set; }
        public bool? Buy { get; set; }
        public int? Money { get; set; }
        public int? WithWhoIdPerson { get; set; }
        public string ItemToBring { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public string Num { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }

        public BaseEvent BaseEvent { get; set; }
        public Person Person { get; set; }
    }
}
