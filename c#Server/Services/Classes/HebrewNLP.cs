using HebrewNLP;
using HebrewNLP.Morphology;
using HebrewNLP.Preprocess;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Services
{
    public class Hebrewnlp
    {
        
        private string Last_Sentence="";
        private string This_Sentence="";
        private string After_Sentence="";
     

        public List<List<string>> ListListsentences(string text)
        {
            
            List<List<string>> lls=new List<List<string>>();
            //פונקציה שמחלקת למשפטים
            // List<string> options = Sentencer.Sentences(text);
            string[] options = text.Split('.', '!', '?');
            int numofword;//מיקום של מילה במשפט 

            //לולאה שעוברת על כל משפטים
            for (int i = 0; i < (options.Length); i++)
            {
                numofword = 0;
                //פונקציה שמנתחת את כל המילים בכל משפט
                List<List<MorphInfo>> optionss = HebrewMorphology.AnalyzeSentence(options[i]);
                //בשביל הזמן
                //לולאה שעוברת על כל המילים בכל משפט
                foreach (var item in optionss)
                {
                    //בודק אם למילה יש אותיות מזה וכלב
                    if (item[0].PrepositionChars == PrepositionChars.BET || item[0].PrepositionChars == PrepositionChars.KAF || item[0].PrepositionChars == PrepositionChars.LAMED || item[0].PrepositionChars == PrepositionChars.MEM|| item[0].DefiniteArticle == true|| item[0].Subordination == Subordination.SHE|| item[0].Vav==true)
                    {
                        //לעשות לפי 2 ספליט
                        string[] arrw = options[i].Split(' ', '/', '.', '-', ',', '_','|','\\');
                        //מחלק את המשפט המקורי למערך מילים
                        options[i] = "";
                        string wo = "";
                        int index = 0;
                        foreach (var s in arrw[numofword])// לולאה שעוברת על האותיותת של המילה ומורידה את האות הראשונה 
                        {
                            if (index != 0)
                                wo += s;//המילה החדשה ללא בכל"מ
                            index = 1;
                        }

                        arrw[numofword] = wo;//מכניסה למערך העזר את המילה החדשה ללא בכל"מ
                        for (int d = 0; d < arrw.Length; d++)
                            options[i] += arrw[d] + " ";//משרשרת את המשפט העדכני לרשימת המשפטים המקוריים

                    }
                    numofword++;
                }
              }
            for (int j = 0; j < (options.Length);j++)
            {
                List<List<MorphInfo>> optionss = HebrewMorphology.AnalyzeSentence(options[j]);
                foreach (var item in optionss)
                {
                    //בדיקה אם המילה היא פועל בעתיד או שם פועל
                    if (item[0].PartOfSpeech == PartOfSpeech.VERB && (item[0].Tense == Tense.FUTURE || item[0].Tense == Tense.INFINITIVE))
                    {
                        List<string> ls = new List<string>();
                        After_Sentence = "";
                        //צריך לשמור ב3 משתנים את המשפט לפני אחרי והמשפט בעצמו
                        if (j != 0)
                        {
                            Last_Sentence = options[j - 1];

                        }
                        This_Sentence = options[j];
                        if (j+1 != options.Length)
                        { After_Sentence = options[j + 1]; }

                        ls.Add(Last_Sentence);
                        ls.Add(This_Sentence);
                        ls.Add(After_Sentence);
                        lls.Add(ls);                        
                        break;
                    }
                }
            }
            return lls;
        }  
    }
}
