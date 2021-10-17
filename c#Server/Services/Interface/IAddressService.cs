
using Common1;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IAddressService
    {
        CAddress GetAddressFromText(string str, string language);
    }
}
