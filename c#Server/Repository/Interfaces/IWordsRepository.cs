using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IWordsRepository
    {
        void Add_Words(Words w);
        void Delete_Words(Words w);
        void Edit_Words(Words w);
        List<Words> GetListWords();
        Words GetWord(int? id);
    }
}
