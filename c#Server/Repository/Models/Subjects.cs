using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class Subjects
    {
        public Subjects()
        {
            WordsToSubjects = new HashSet<WordsToSubjects>();
        }

        public int IdSubject { get; set; }
        public string NameSubject { get; set; }

        public ICollection<WordsToSubjects> WordsToSubjects { get; set; }
    }
}
