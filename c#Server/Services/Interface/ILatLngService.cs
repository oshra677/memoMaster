using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interface
{
    public interface ILatLngService
    {
        void FindDistance(ref double lat, ref double lng, string from);
    }
}
