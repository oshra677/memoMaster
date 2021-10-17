using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface ICategoryCommunicationRepository
    {
        void Add_Communication(CategoryCommunication w);
    }
}
