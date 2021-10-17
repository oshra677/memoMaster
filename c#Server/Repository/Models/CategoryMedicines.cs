using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CategoryMedicines
    {
        public int IdCategoryMedicines { get; set; }
        public string NameMedicines { get; set; }
        public int? FrequencyWeek { get; set; }
        public int? FrequencyDay { get; set; }
        public int? FrequencyMonth { get; set; }
        public TimeSpan? Afternoon { get; set; }
        public TimeSpan? Morning { get; set; }
        public TimeSpan? Evening { get; set; }

        public BaseEvent BaseEvent { get; set; }
    }
}
