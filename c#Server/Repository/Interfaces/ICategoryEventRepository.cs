using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface ICategoryEventRepository
    {
        void Add_Event(CategoryEvent w);
    }
}
