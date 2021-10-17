using System;
using System.Collections.Generic;
using System.Text;
using HebrewNLP;
using HebrewNLP.Morphology;

namespace Services.Interface
{
    public interface IHebrew_NLPService
    {
        List<List<string>> ListListsentences(string text);
    }
}
