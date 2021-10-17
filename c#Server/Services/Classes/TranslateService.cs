using Google.Cloud.Translation.V2;
using Services_Fw;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class TranslateService : ITranslateService
    {      
        //פונקציה שמחזירה באיזה שפה הטקסט
        public string DetectLanguage(string text)
        {
            TranslationClient client = TranslationClient.Create();
            var detection = client.DetectLanguage(text: text);
            return ($"{detection.Language}");
        }
        //פונקציה שמתרגמת טקסט משפה כלשהי לעיברית
        public string TranslateTextToHebrew(string text, string language)
        {
            //אם הטקסט בעברית אז LANGUAGE=IW
            if (language!="iw")
            {
                TranslationClient client = TranslationClient.Create();
                var response = client.TranslateText(
                    text: text,
                    targetLanguage: "he",  // Hebrew
                    sourceLanguage: language);  // language
                //הטקסט שתורגם לעברית
                text = response.TranslatedText;
            }
            return text;
        }
        //פונקציה שמתרגמת טקסט מעברית לשפה כלשהי
        public string TranslateTextFromHebrew(string text,string language)
        {
            if (language != "iw")
            {
                TranslationClient client = TranslationClient.Create();
                var response = client.TranslateText(
                    text: text,
                    targetLanguage: language,  // language
                    sourceLanguage: "he");  // language
                text = response.TranslatedText;
            }
            return text;
        }
        //פונקציה שמתרגמת טקסט משפה כלשהי לאנגלית
        public string TranslateTextToEnglish(string text, string language)
        {
            //אם הטקסט בעברית אז LANGUAGE=IW
            if (language != "en")
            {
                TranslationClient client = TranslationClient.Create();
                var response = client.TranslateText(
                    text: text,
                    targetLanguage: "en",  // Hebrew
                    sourceLanguage: language);  // language
                //הטקסט שתורגם לאנגלית
                text = response.TranslatedText;
            }
            return text;

        }
        // פונקציה שמתרגמת טקסט מאנגלית לשפה כלשהי
        public string TranslateTextFromEnglish(string text, string language)
        {
            //אם הטקסט בעברית אז LANGUAGE=IW
            if (language != "en")
            {
                TranslationClient client = TranslationClient.Create();
                var response = client.TranslateText(
                    text: text,
                    targetLanguage: language,  // language
                    sourceLanguage: "en");  // english
                text = response.TranslatedText;
            }
            return text;

        }




    }
}
