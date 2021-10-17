using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CategoryEvent
    {
        public int IdCategoryEvent { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public string Num { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public int? WithWhoIdPerson { get; set; }
        public string ItemToBring { get; set; }
        public int? WhoseIdPerson { get; set; }

        public BaseEvent BaseEvent { get; set; }
        public Person Person { get; set; }
        public Person PersonNavigation { get; set; }
    }
}
