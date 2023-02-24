using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;

public class dictionaryOld : MonoBehaviour
{
    public InputField engWordField;
    public InputField ruWordField;

    public string eng;
    public string ru;

    public List<string> engWord;
    public List<string> ruWord;
    public GameObject[] clearDictionary;

    public Text warning;
    public Text dictionaryText;

    public Text engWordTextOut;
    public Text rusWordTextOut;

    public Text newText;

    Text tempText;
    string forText;
    string forIndex;

    int countEn = 0;
    int countRu = 0;
    int countTotal = 0;
    int index;

    string[] dictEnglishMassive;
    string[] dictRussianMassive;
    string[] dictEnglishMassiveWrite;
    string[] dictRussianMassiveWrite;
    string[] separators = { "\r\n" };
    string toEn;
    string toRus;
    //string toMassiveEn;
    //string toMassiveRus;


    string dictEngl;
    string assetPath;
    string _query_TOP;
    string filePath;

    string[] engMassive = {"A", "a", "B", "b", "C", "c", "D", "d", "E", "e", "F", "f", "G", "g", "H", "h",
    "I", "i", "J", "j", "K", "k", "L", "l", "M", "m", "N", "n", "O", "o", "P", "p", "Q", "q", "R", "r", "S", "s",
    "T", "t", "U", "u", "V", "v", "W", "w", "X", "x", "Y", "y", "Z", "z"};

    string[] ruMassive = {"А", "а", "Б", "б", "В", "в", "Г", "г", "Д", "д", "Е", "е", "Ё", "ё", "Ж", "ж",
    "З", "з", "И", "и", "Й", "й", "К", "к", "Л", "л", "М", "м", "Н", "н", "О", "о", "П", "п", "Р", "р", "С", "с",
    "Т", "т", "У", "у", "Ф", "ф", "Х", "х", "Ц", "ц", "Ч", "ч", "Ш", "ш", "Щ", "щ", "Ь", "ь", "Ы", "ы", "Ъ", "ъ",
    "Э", "э", "Ю", "ю", "Я", "я"};

    void Start()
    {
        StartCoroutine(LoadText("dictionaryEn.txt", "dictionaryRus.txt"));

        //Debug.Log(Application.persistentDataPath);

        //string[] dictE = File.ReadAllLines(Application.dataPath + "/Standard Assets/dictionaryEn.txt"); //массив который берётся из текстового файла
        //string[] dictR = File.ReadAllLines(Application.dataPath + "/Standard Assets/dictionaryRus.txt"); //массив который берётся из текстового файла


        //for (int i = 0; i < dictE.Length; i++)
        //{
        //    engWordTextOut.text = dictE[Random.Range(0, dictE.Length)];
        //}
        //index = System.Array.IndexOf(dictE, engWordTextOut.text);

        //for (int i = 0; i < dictR.Length; i++)
        //{
        //    rusWordTextOut.text = dictR[index];
        //}

        ///////////////////
        //TextAsset textAssetDictEnglish = Resources.Load<TextAsset>("dictionaryEn"); //загружает dictionaryEn.txt из папки Resources
        //                                                                            //(работает и windows и в android)

        //toMassiveEn = textAssetDictEnglish.ToString();                              //string toMassive принимает TextAsset textAsset

        //dictEnglish = toMassiveEn.Split(separators, System.StringSplitOptions.RemoveEmptyEntries); //разбивает string toMassive на строки,
        //                                                                                           //и эти строки принимает string[] dictEnglish

        ////делает тоже самое, что и 3 строки выше
        //TextAsset textAssetDictRussian = Resources.Load<TextAsset>("dictionaryRus");
        //toMassiveRus = textAssetDictRussian.ToString();
        //dictRussian = toMassiveRus.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);


        //for (int i = 0; i < dictEnglish.Length; i++)
        //{
        //    engWordTextOut.text = dictEnglish[Random.Range(0, dictEnglish.Length)];
        //}
        //index = System.Array.IndexOf(dictEnglish, engWordTextOut.text);

        //for (int i = 0; i < dictRussian.Length; i++)
        //{
        //    rusWordTextOut.text = dictRussian[index];
        //}
        //////////////////
    }

    void Update()
    {
        clearDictionary = GameObject.FindGameObjectsWithTag("words");
    }

    //для загрузки файлов "dictionaryEn.txt", "dictionaryRus.txt"
    IEnumerator LoadText(string fileName, string fileName1)
    {
        string path = Application.streamingAssetsPath + "/" + fileName;
        string path1 = Application.streamingAssetsPath + "/" + fileName1;

        if (Application.isMobilePlatform)
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(path);
            UnityWebRequest webRequest1 = UnityWebRequest.Get(path1);

            yield return webRequest.SendWebRequest();
            yield return webRequest1.SendWebRequest();

            engWordTextOut.text = webRequest.downloadHandler.text;
            rusWordTextOut.text = webRequest1.downloadHandler.text;

            /////////
            toEn = engWordTextOut.text;
            dictEnglishMassive = toEn.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);

            toRus = rusWordTextOut.text;
            dictRussianMassive = toRus.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < dictEnglishMassive.Length; i++)
            {
                engWordTextOut.text = dictEnglishMassive[Random.Range(0, dictEnglishMassive.Length)];
            }
            index = System.Array.IndexOf(dictEnglishMassive, engWordTextOut.text);

            for (int i = 0; i < dictRussianMassive.Length; i++)
            {
                rusWordTextOut.text = dictRussianMassive[index];
            }
        }
        else
        {
            engWordTextOut.text = File.ReadAllText(path);
            rusWordTextOut.text = File.ReadAllText(path1);

            /////////
            toEn = engWordTextOut.text;
            dictEnglishMassive = toEn.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);

            toRus = rusWordTextOut.text;
            dictRussianMassive = toRus.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < dictEnglishMassive.Length; i++)
            {
                engWordTextOut.text = dictEnglishMassive[Random.Range(0, dictEnglishMassive.Length)];
            }
            index = System.Array.IndexOf(dictEnglishMassive, engWordTextOut.text);

            for (int i = 0; i < dictRussianMassive.Length; i++)
            {
                rusWordTextOut.text = dictRussianMassive[index];
            }
        }
    }


    public void loadWord()
    {
        newText.text = File.ReadAllText(Application.persistentDataPath + "/dictionaryEn.txt");
        File.ReadAllText(Application.persistentDataPath + "/dictionaryRus.txt");
    }

    public void saveWord()
    {
        string textWord = "keks";
        File.WriteAllText(Application.persistentDataPath + "/dictionaryEn.txt", textWord);
        File.WriteAllText(Application.persistentDataPath + "/dictionaryRus.txt", textWord);
    }
    
    //для сохранения в файл "dictionaryRus.txt"
    //IEnumerator saveRuText(string fileName)
    //{
    //    string path = Application.streamingAssetsPath + "/" + fileName;

    //    if (Application.isMobilePlatform)
    //    {
    //        UnityWebRequest webRequest = UnityWebRequest.Get(path);

    //        yield return webRequest.SendWebRequest();

    //        rusWordTextOut.text = webRequest.uploadHandler.contentType;

    //        //здесь передать string path в массив а этот массив использовать в функции для записи 
    //    }
    //    else
    //    {
    //        File.AppendAllText(path, "\r\n");
    //        File.AppendAllText(path, ru);
    //    }
    //}

    //для сохранения в файл "dictionaryEn.txt"
    //IEnumerator saveEnText(string fileName)
    //{
    //    string path = Application.streamingAssetsPath + "/" + fileName;

    //    if (Application.isMobilePlatform)
    //    {
    //        UnityWebRequest webRequest = UnityWebRequest.Get(path);

    //        yield return webRequest.SendWebRequest();

    //        engWordTextOut.text = webRequest.uploadHandler.contentType;
    //    }
    //    else
    //    {
    //        File.AppendAllText(path, "\r\n");
    //        File.AppendAllText(path, ru);
    //    }
    //}

    //для добавления новых слов в словари и массивы (кнопка ОК)
    public void addWords()
    {
        ru = ruWordField.text;
        eng = engWordField.text;

        //string dictEn = Application.dataPath + "/Standard Assets/dictionaryEn.txt"; //путь к файлу словаря
        //string dictRus = Application.dataPath + "/Standard Assets/dictionaryRus.txt"; //путь к файлу словаря

        //string[] dictE = File.ReadAllLines(Application.dataPath + "/Standard Assets/dictionaryEn.txt"); //массив который берётся из текстового файла
        //string[] dictR = File.ReadAllLines(Application.dataPath + "/Standard Assets/dictionaryRus.txt"); //массив который берётся из текстового файла


        StartCoroutine(LoadText("dictionaryEn.txt", "dictionaryRus.txt"));

        /* функция проверки "пустые поля для заполнения или нет"(если пустые то  warning.text = "Заполните оба поля").
           Так-же проверяет, присутствуют русские симолы в английском
           поле или нет (если присутствуют, то warning.text = "Только английские символы").
           Присутствуют английские симолы в русском поле или нет (если присутствуют, то warning.text = "Только русские символы").
           Если-же все условия соблюдены правильно, то warning.text = "" и функция работает дальше */
        for (int i = 0; i < engMassive.Length; i++)
        {
            for (int k = 0; k < ruMassive.Length; k++)
            {
                if (eng == "" || ru == "")
                {
                    warning.text = "Заполните оба поля";
                }
                else
                {
                    if (ru != "" || eng != "")
                    {
                        if (ru.Contains(engMassive[i]))
                        {
                            warning.text = "Только русские символы";
                        }
                        if (eng.Contains(ruMassive[k]))
                        {
                            warning.text = "Только английские символы";
                        }
                        else
                        {
                            if (ru != "" || eng != "")
                            {
                                if (eng.Contains(engMassive[i]) && ru.Contains(ruMassive[k]))
                                {
                                    warning.text = "";

                                    countTotal = 1; //запускает следующую функцию

                                    StartCoroutine(wait());
                                    IEnumerator wait()
                                    {
                                        yield return new WaitForSeconds(0.1f);
                                        countTotal = 0;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /* функция добавления новых слов в файлы словарей и массивы введённых слов 
           (если слово уже есть в файлах словаря, либо в массивах словаря, то слово
           не добавляется ни в файлы словаря, ни в массивы слов) */
        if (countTotal == 1)
        {
            //для английского языка
            foreach (string a in engWord) //этот цикл проверяет лист engWord на повторы 
                                          //(если слова в engWord поторяются, то  warning.text = "Это слово есть в словаре")
            {
                if (a.Contains(eng)) //если string a содержит хоть одну такую-же запись как в List<string> engWord
                {

                    if (eng.Length > 0) //если длинна string eng больше ноля
                    {
                        warning.text = "Это слово есть в словаре";
                        eng = "";
                    }
                }     
            }
            for (int i = 0; i < dictEnglishMassive.Length; i++) //этот цикл проверяет файл английского словаря на повторы 
                                                   //(если слова в файле английского словаря поторяются, 
                                                   //то warning.text = "Это слово есть в словаре")
            {
                if (eng == dictEnglishMassive[i])
                {
                    countEn = 1;
                    warning.text = "Это слово есть в словаре";
                }

                StartCoroutine(wait());
                IEnumerator wait()
                {
                    yield return new WaitForSeconds(0.1f);
                    countEn = 0;
                }
            }
            if (countEn == 0 && eng.Length > 0)
            {
                engWord.Add(eng);

                //место для функции добавления слов

                engWordField.text = "";
                warning.text = "";
            }
            //для русского языка
            foreach (string b in ruWord) //этот цикл проверяет лист ruWord на повторы 
                                         //(если слова в engWord поторяются, то  warning.text = "Это слово есть в словаре")
            {
                if (b.Contains(ru)) //если string b содержит хоть одну такую-же запись как в List<string> ruWord
                {
                    if (ru.Length > 0) //если длинна string ru больше ноля
                    {
                        warning.text = "Это слово есть в словаре";
                        ru = "";
                    }
                }
            }
            for (int i = 0; i < dictRussianMassive.Length; i++) //этот цикл проверяет файл русского словаря на повторы 
                                                   //(если слова в файле русского словаря поторяются, 
                                                   //то warning.text = "Это слово есть в словаре")
            {
                if (ru == dictRussianMassive[i])
                {
                    countRu = 1;
                    warning.text = "Это слово есть в словаре";
                }

                StartCoroutine(wait1());
                IEnumerator wait1()
                {
                    yield return new WaitForSeconds(0.1f);
                    countRu = 0;
                }
            }
            if (countRu == 0 && ru.Length > 0)
            {
                ruWord.Add(ru);

                //место для функции добавления слов

                ruWordField.text = "";
                warning.text = "";
            }
        }

      

        //if (eng == "" || ru == "")
        //{
        //    warning.text = "Заполните оба поля";
        //}
        //else
        //{
        //    warning.text = "";
        //}

        //for (int i = 0; i < engMassive.Length; i++)
        //{
        //    if (ru != "")
        //    {
        //        if (ru.Contains(engMassive[i]))
        //        {
        //            warning.text = "Только русские символы";
        //        }
        //    }
        //}

        //for (int i = 0; i < ruMassive.Length; i++)
        //{
        //    if (eng != "")
        //    {
        //        if (eng.Contains(ruMassive[i]))
        //        {
        //            warning.text = "Только английские символы";
        //        }
        //    }
        //}
    }

    
    
    //для создания массива словаря (конпка "Словарь")
    public void createListWord()
    {
        StartCoroutine(wait());
        IEnumerator wait()
        {
            yield return new WaitForSeconds(0.1f);

            string[] dictE = File.ReadAllLines(Application.dataPath + "/Standard Assets/dictionaryEn.txt"); //массив который берётся из текстового файла
            string[] dictR = File.ReadAllLines(Application.dataPath + "/Standard Assets/dictionaryRus.txt"); //массив который берётся из текстового файла

            for (int i = 0; i < dictE.Length; i++)
            {
                forText = dictE[i] + " - " + dictR[i];

                tempText = Instantiate(dictionaryText);
                tempText.transform.SetParent(GameObject.Find("Content").transform);
                tempText.transform.localScale = new Vector3(1, 1, 1);
                tempText.text = forText;
            }
        }
    }

    //для очистки массива словаря (кнопка "Закрыть" на панели полного словаря)
    public void clearListWord()
    {
        for (int i = 0; i < clearDictionary.Length; i++)
        {
            Destroy(clearDictionary[i]);
        }  
    }

    //для рандомного вывода слов из словаря в тексты UI (кнопка "Rotate")
    public void outputWords()
    {
        //string[] dictE = File.ReadAllLines(Application.dataPath + "/Standard Assets/dictionaryEn.txt"); //массив который берётся из текстового файла
        //string[] dictR = File.ReadAllLines(Application.dataPath + "/Standard Assets/dictionaryRus.txt"); //массив который берётся из текстового файла

        //for (int i = 0; i < dictE.Length; i++)
        //{
        //    engWordTextOut.text = dictE[Random.Range(0, dictE.Length)];
        //}
        //index = System.Array.IndexOf(dictE, engWordTextOut.text);

        //for (int i = 0; i < dictR.Length; i++)
        //{
        //    rusWordTextOut.text = dictR[index];
        //}



        ///////////////////
        TextAsset textAssetDictEnglish = Resources.Load<TextAsset>("dictionaryEn"); //загружает dictionaryEn.txt из папки Resources
                                                                                    //(работает и windows и в android)

        toEn = textAssetDictEnglish.ToString();                              //string toMassive принимает TextAsset textAsset

        dictEnglishMassive = toEn.Split(separators, System.StringSplitOptions.RemoveEmptyEntries); //разбивает string toMassive на строки,
                                                                                                   //и эти строки принимает string[] dictEnglish

        //делает тоже самое, что и 3 строки выше
        TextAsset textAssetDictRussian = Resources.Load<TextAsset>("dictionaryRus");
        toRus = textAssetDictRussian.ToString();
        dictRussianMassive = toRus.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);


        for (int i = 0; i < dictEnglishMassive.Length; i++)
        {
            engWordTextOut.text = dictEnglishMassive[Random.Range(0, dictEnglishMassive.Length)];
        }
        index = System.Array.IndexOf(dictEnglishMassive, engWordTextOut.text);

        for (int i = 0; i < dictRussianMassive.Length; i++)
        {
            rusWordTextOut.text = dictRussianMassive[index];
        }
        //////////////////
    }

    //для очистки InputField (кнопка "Закрыть" на панели добавить слова)
    public void clearInputFields()
    {
        engWordField.text = "";
        ruWordField.text = "";
    }
}