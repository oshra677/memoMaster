using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public partial class WordsTime
    {
        public int IdWordsTime { get; set; }
        public string Word { get; set; }
        public string Type { get; set; }
        public string FuncHowMany { get; set; }
        public string Argument { get; set; }
    }
}
