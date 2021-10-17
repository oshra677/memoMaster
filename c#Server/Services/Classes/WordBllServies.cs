
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;
using Repository;
using Repository.Models;
using HebrewNLP.Preprocess;
using HebrewNLP.Morphology;
using Services.Interface;

namespace Services.Classes
{
    internal delegate object Predicate<in T>();
    public class WordBllServies: IWordBllService
    {
        public static Hashtable hashtableword = new Hashtable();

        public static Hashtable hashtablesubjectToWord = new Hashtable();
        const int n = 7;//מספר הנושאים
        const double p = 0.25;
        private IWordsToSubjectsRepository WordsToSubjectsRepository;
        private IWordsRepository WordsRepository;
        private ISubjectsRepository SubjectsRepository;
        public WordBllServies(IWordsToSubjectsRepository WordsToSubjectsRepository, IWordsRepository WordsRepository, ISubjectsRepository SubjectsRepository)
        {
            this.WordsToSubjectsRepository = WordsToSubjectsRepository;
            this.WordsRepository = WordsRepository;
            this.SubjectsRepository = SubjectsRepository;

        }

        public  string subject( List<string> args,bool ca)
        {
            FromDataBaseToHash();
            int i;
            int j;
            WordsToSubjects subjectToWord;
            double m = args.Count;//מספר המילים במשפט
            double[,] arrw = new double[1, (int)m];//מערך של כל המילים
            int[,] arr = new int[(int)m, n];//מערך של כל מילה בכל הנושאים
            for (i = 0; i < m; i++)
            {
                for (j = 0; j < n; j++)
                {
                    arr[i, j] = 0;
                }
            }
            arrw[0, (int)m / 2] = m;//המשקל של המילה האמצעית לפי מספר המילים
            Words word;
            if (m > 1)
            {
                if (m % 2 == 1)//אם מספר המילים אי זוגי
                    arrw[0, ((int)m + 1) / 2] = m;//המשקל של המילה אחרי המילה האמצעית שווה למספר המילים
                    arrw[0, ((int)m / 2) - 1] = m;//המשקל של המילה לפני המילה האממעית שווה למספר המילים
                if (m > 2)//אם מספר המילים גדול מ 2
                {
                    for ( i =( (int)m/2)-2; i >=0 ; i--)
                    {
                        arrw[0, i] = arrw[0, i + 1] - ((m / ((m - 4)) / 2.0));
                        arrw[0, (int)m - i - 1] = arrw[0, i];
                    }
                    i = i+1;
                    if (arrw[0, i] < 0)//אם המשקל של מילה מסוימת קטנה מ0
                    {
                        arrw[0, i] = 1;
                        arrw[0, (int)m - i - 1] = 1;
                    }
                }
            }
            for (i = 0; i < m; i++)
            {
                word = (Words)hashtableword[args[i]];//מחפש בטבלת האהש אם המילה קיימת
                if (word == null)//אם המילה חדשה
                {
                    Words word1 = new Words();
                    word1.NameWord = args[i];
                    word1.Frequency = 1;//למילה חדשה כמובן התדירות שלה 1
                    WordsRepository.Add_Words(word1);//מוסיף מילה חדשה לטבלת המילים
                    word = WordsRepository.GetListWords().Last();
                    hashtableword.Add(word.NameWord, word);//מוסיף את המילה החדשה לטבלת האהש
                    continue;
                }
                //אם המילה כבר קיימת מעלה את התדירות שלה 
                word.Frequency += 1;
                for ( j = 1; j < n; j++)
                {
                   subjectToWord = (WordsToSubjects)hashtablesubjectToWord[j + word.NameWord];//מחפש בהאש אם המילה לנושא נמצאת
                    if (subjectToWord != null)// אם מצא נושא למילה הזאת, מכניס למילה הזאת בנושא הזה את הציון הקודם
                    {
                        arr[i, j] = (int)(subjectToWord.Mark);
                    }
                    else
                    {
                        arr[i, j] = 0;
                    }

                }
                //מעדכן את האהש כי התדירות שלהמילה השתנתה
                hashtableword[word.NameWord] = word;
            }
            //החלטת ציון
            #region
            int[,] decision = new int[2, (int)m];
            int indexmax = 0;
            for (int x = 0; x < m; x++)
            {
                indexmax = 0;
                for (int y = 0; y < n; y++)
                {

                    if (arr[x, y] > arr[x, indexmax])//מחפש את הציון הכי גבוה של כל מילה מבין כל הנושאים
                    {
                        indexmax = y;
                    }
                    //לחפש הכרעה כלומרלהכניס למערך החלטה את האינדקס של השורה שהעמ
                }
                if (indexmax == 0 && ca == true)
                {
                    hashtableword.Remove(hashtableword[args[x]]);                   
                }
                else
                {
                    decision[0, x] = indexmax;//מכניס עבור כל מילה את מספר הנושא עם הציון הגבוה במילה זאת
                    decision[1, x] = (int)arr[x, indexmax];//מכניס עבור כל מילה את הציון של המילה הזאת בנושא שהיה לו את הציון הכי גבוה
                }       
            }
            //בקיצור השורה הראשונה של המערך לכל מילה יש את מספר הנושא עם הציון הכי גבוה  והשורה השניה זה כל מילה עם הציון הקודם של המילה בנושא שהחלט
            int[,] decisionfinish = new int[3, n];//מטריצה עם שלוש שורות ועמודות כמספר הנושאים
            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < n; j++)
                {
                    decisionfinish[i, j] = 0;
                }
            }
            // לולאה שעוברת על כל המילים במטריצה דיסיסיון ובודקת את הציון שלה
            for (int x = 0; x < m; x++)
            {
                if (decision[1, x] == 100&&ca!=true)
                    return "Cancel";
                 
                if (decision[1, x] >= 90)//אם הציון של המילה גבוה מ90 
                {
                    decisionfinish[0, decision[0, x]]++;//מעלה ב1 בשורה הראשונה בעמודה של הנושא של המילה
                }
                else
                {
                    if (decision[1, x] >= 80)
                    {
                        decisionfinish[1, decision[0, x]]++;//מעלה ב1 בשורה השניה בעמודה של הנושא של המילה
                    }
                    else
                    {
                        if (decision[1, x] >= 70)
                        {
                            decisionfinish[2, decision[0, x]]++;//מעלה ב1 בשורה השלישית בעמודה של הנושא של המילה
                        }
                    }
                }
            }
            j = -1;
            for (i = 0; i < 3; i++)
            {
                for (int t = 0; t < n; t++)
                {
                    if (decisionfinish[i, t] >= i + 1)//על כל נושא בודק כמה מילים יש לו
                    //בשורה הראשונה בודק אם מספר המילים לנושא גדול שווה ל1
                    //בשורה השניה בודק אם מספר המילים לנושא גדול שווה ל2
                    //בשורה השלשית בודק אם מספר המילים לנושא גדול שווה ל3
                    //תזכורת: שורה ראשונה מראה את המילים בעלי ציון מעל 90
                    //שורה שניה מראה את המילים בעלי ציון מעל 80
                    //שורה שלישית מראה את המילים בעלי ציון מעל 70 
                    {
                        if (j == -1)//אם זה בפעם הראשונה מאתחלים את j
                           j = t; 
                        else//אם זה לא בפעם הראשונה שמצא ציון עובר
                        {
                            if (decisionfinish[i, t] > decisionfinish[i, j])//מכניס ל j את הנושא עם המספר המילים הגבוה ביותר
                                j = t;
                            else
                            {
                                if (decisionfinish[i, t] == decisionfinish[i, j])//אם מספר המילים בנושא שווה למספר המילים הקודם שנמצא בJ 
                                {
                                    if (decisionfinish[0, t] + decisionfinish[1, t] + decisionfinish[2, t] > decisionfinish[0, j] + decisionfinish[1, j] + decisionfinish[2, j])
                                        //אם כל סכום העמודה של הנושא גדולה מסכום העמודה הקודם J אז שומר את מספר השורה בו נמצאים
                                    { j = i; }
                                    else
                                        if (decisionfinish[0, t] + decisionfinish[1, t] + decisionfinish[2, t] == decisionfinish[0, j] + decisionfinish[1, j] + decisionfinish[2, j])
                                        //אם כל סכום העמודה של הנושא שווה לסכום העמודה הקודם  J 
                                        j = -2;
                                }
                            }

                        }
                    }
                }
                if (j != -1)// אם נכנס ערך לJ
                { break; }
            }


            if (j == -1 || j == -2)//אם יתקבל הערך -2 זה אומר שיש שתי נושאים בעלי ציון זהה ולכן אין נושא מכריע ואם הערך בגיי נשאר -1 זה אומר שאין שום ציון מכריע כלומר אין מילה אחת מעל 90 או שתי מילים מעל 80 או שלוש מילים מעל 70 
             return stateListsService.משפט_לא_מכריע.ToString(); 
            #endregion  //החלטת ציון

            for (i = 0; i < m; i++)
            {
                word = (Words)hashtableword[args[i]];
                //  מחפש על כל מילה האם היא קיימת במילה לנושא המכריע
                subjectToWord = (WordsToSubjects)hashtablesubjectToWord[j + word.NameWord];
                if (subjectToWord == null)
                {
                    //הוספת המילה לנושא המכריע
                    WordsToSubjects subjectToWord1 = new WordsToSubjects();
                    subjectToWord1.FrequencyWordToSubject = 0;
                    subjectToWord1.IdSubject = j;
                    subjectToWord1.IdWord = word.IdWord;
                    subjectToWord1.Mark = 50;
                    WordsToSubjectsRepository.Add_WordsToSubject(subjectToWord1);
                    subjectToWord = WordsToSubjectsRepository.GetListWordsToSubject().Last();
                    hashtablesubjectToWord.Add(j + word.NameWord, subjectToWord);
                }
                subjectToWord.FrequencyWordToSubject += 1;
                int M = 0;
                for (int l = 0; l < n; l++)
                {
                    if ((WordsToSubjects)hashtablesubjectToWord[l + word.NameWord] != null && ((WordsToSubjects)hashtablesubjectToWord[l + word.NameWord]).FrequencyWordToSubject >= p * subjectToWord.FrequencyWordToSubject)//צריך לבדוק אם השורה הזאת נכונה
                    {
                        M++;
                    }
                }

                subjectToWord.Mark = (int)(subjectToWord.Mark + (int)((double)(subjectToWord.FrequencyWordToSubject / (double)word.Frequency) * 50) + (((double)(n - M + 1) / (double)n) * 35) + (int)(arrw[0, i] / m * 15)) / 2;

                //מעדכן בהאש כי עדכנו את הציון
                hashtablesubjectToWord[subjectToWord.IdSubject + word.NameWord] = subjectToWord;
            }
            // ViewService.Markes = new Elemnt() { Descraption = "לאחר שמילא את המטריצה הנל הוא בודק עבור מי יש סכום הכי גבהה של ציונים מעל 90, אם זהה מחפש מעל 80, ואם גם זה זזהה עובר לחפש כמה מעל 70, אם לא מצא אין הכרעה. לאחר שהכריע את הנושא הוא עובר על כל מילה ומעדכן את ציונה בהתאם לנושא זה, למעשה הציון של כל מילה יעלה בנושא המתאים. כדי לשמור על עדכנות לנושא הבא, הציון משתקלל על פי הנוסחא הבאה(int)(subjectToWord.ziun + (int)((double)(subjectToWord.frequency / (double)word.frequency) * 50) + (((double)(n - M + 1) / (double)n) * 35) + (int)(arrw[0,i] / m * 15)) / 2" };
            FromHashToDataBase();//מעדכן מהאש לדטה בייס
            return SubjectsRepository.GetListSubjects().Find(a => a.IdSubject == j).NameSubject;//מחזיר לבסוף את שם הנושא

        }
      

        public void FromDataBaseToHash()//פונקציה שמטעינה את האהש בכל המילים שבדטה בייס
        {  
            foreach (var WordFromList in WordsRepository.GetListWords())
            {
                if (!hashtableword.ContainsKey(WordFromList.NameWord))//אם המילה לא נמצאת בטבלת hash
                {
                    hashtableword.Add(WordFromList.NameWord, WordFromList);//הוספת המילה לטבלת הhash
                    int i = hashtableword[WordFromList.NameWord].GetHashCode();
                }
            }

            foreach (var WoToSubFromList in WordsToSubjectsRepository.GetListWordsToSubject())
            {
                //מטעין את הטבלה של המילים לנושא להאש
                if (!hashtablesubjectToWord.ContainsKey(WoToSubFromList.IdSubject + WordsRepository.GetWord(WoToSubFromList.IdWord).NameWord))
                {
                    hashtablesubjectToWord.Add(WoToSubFromList.IdSubject + WordsRepository.GetWord(WoToSubFromList.IdWord).NameWord, WoToSubFromList);
                }
            }

        }
        public void FromHashToDataBase()
        {
            // לולאה שעוברת על כל המילים הקיימות שבדטה בייס ומעדכנת את המילה לפי האהש
            foreach (var WordFromList in WordsRepository.GetListWords()) 
            {
                Words we = (Words)hashtableword[WordFromList.NameWord];

                WordsRepository.Edit_Words(we);
            }
            // לולאה שעוברת על כל המילים לנושאים ומעדכנת לפי האהש
            foreach (var WoToSubFromList in WordsToSubjectsRepository.GetListWordsToSubject())
            {
                WordsToSubjects we = (WordsToSubjects)hashtablesubjectToWord[WoToSubFromList.IdSubject + WordsRepository.GetWord(WoToSubFromList.IdWord).NameWord];

                WordsToSubjectsRepository.Edit_WordsToSubject(we);
            }


        }

    }
}

