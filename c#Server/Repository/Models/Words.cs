using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class Words
    {
        public Words()
        {
            WordsToSubjects = new HashSet<WordsToSubjects>();
        }

        public int IdWord { get; set; }
        public string NameWord { get; set; }
        public int? Frequency { get; set; }

        public ICollection<WordsToSubjects> WordsToSubjects { get; set; }
    }
}
