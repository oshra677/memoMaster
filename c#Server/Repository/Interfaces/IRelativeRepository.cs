
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IRelativeRepository
    {
        void Add_Relative(Relative r);

        void Delete_Relative(Relative r);

        void Edit_Relative(Relative r);
    }
}
