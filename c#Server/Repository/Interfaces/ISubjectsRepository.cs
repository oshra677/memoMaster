using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface ISubjectsRepository
    {
        void Add_Subjects(Subjects s);
        List<Subjects> GetListSubjects();
    }
}
