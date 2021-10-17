using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IWordsToSubjectsRepository
    {
        void Add_WordsToSubject(WordsToSubjects w);
        void Delete_WordsToSubject(WordsToSubjects w);
        void Edit_WordsToSubject(WordsToSubjects w);
        List<WordsToSubjects> GetListWordsToSubject();
    }
}
