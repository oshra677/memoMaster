using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interface
{
    public interface IwordTimeManager
    {
        void updateTime(string s, DateTime date, ref DateTime start, ref TimeSpan start1);
    }
}
