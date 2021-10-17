using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IWordsTimeRepository
    {
        void Edit_WordsTime(WordsTime w);
        void Add_WordsTime(WordsTime w);
        List<WordsTime> GetWordTimes();

    }
}
