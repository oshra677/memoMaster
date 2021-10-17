using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface ITranslateService
    {
       
        string DetectLanguage(string text);
        string TranslateTextToHebrew(string str, string language);
        string TranslateTextToEnglish(string text, string language);

        string TranslateTextFromHebrew(string text, string language);
        string TranslateTextFromEnglish(string text, string language);

    }
}
