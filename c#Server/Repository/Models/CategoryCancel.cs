using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class CategoryCancel
    {
        public int IdCategoryCancel { get; set; }
        public int IdDeleteBaseEvent { get; set; }

        public BaseEvent BaseEvent { get; set; }
    }
}
