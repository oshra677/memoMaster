using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class WordsToSubjects
    {
        public int IdWordsToSubjects { get; set; }
        public int? IdWord { get; set; }
        public int? IdSubject { get; set; }
        public int? FrequencyWordToSubject { get; set; }
        public int? Mark { get; set; }

        public Subjects Subjects { get; set; }
        public Words Words { get; set; }
    }
}
