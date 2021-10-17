using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class Relative
    {
        public Relative()
        {
            Person = new HashSet<Person>();
        }

        public int IdRelative { get; set; }
        public string Description { get; set; }
        public int? LevelWarning { get; set; }

        public ICollection<Person> Person { get; set; }
    }
}
