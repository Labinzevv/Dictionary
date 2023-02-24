using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class dictionary : MonoBehaviour
{
    public InputField engWordField;
    public InputField ruWordField;
    public Text enText;
    public Text ruText;
    public Text enTextOutput;
    public Text ruTextOutput;
    public Text warningText;
    public Text dictionaryText;
    public GameObject[] clearDictionary;
    public string enTextInput;
    public string ruTextInput;
    string text = "";
    public List<string> engWord;
    public List<string> ruWord;
    string[] dictEnglishMassive;
    string[] dictRussianMassive;
    string[] separators = { "\r\n" };

    //для определения принадлежности символов к русскому или английскому языку
    string[] engMassive = {"A", "a", "B", "b", "C", "c", "D", "d", "E", "e", "F", "f", "G", "g", "H", "h",
    "I", "i", "J", "j", "K", "k", "L", "l", "M", "m", "N", "n", "O", "o", "P", "p", "Q", "q", "R", "r", "S", "s",
    "T", "t", "U", "u", "V", "v", "W", "w", "X", "x", "Y", "y", "Z", "z"};
    string[] ruMassive = {"А", "а", "Б", "б", "В", "в", "Г", "г", "Д", "д", "Е", "е", "Ё", "ё", "Ж", "ж",
    "З", "з", "И", "и", "Й", "й", "К", "к", "Л", "л", "М", "м", "Н", "н", "О", "о", "П", "п", "Р", "р", "С", "с",
    "Т", "т", "У", "у", "Ф", "ф", "Х", "х", "Ц", "ц", "Ч", "ч", "Ш", "ш", "Щ", "щ", "Ь", "ь", "Ы", "ы", "Ъ", "ъ",
    "Э", "э", "Ю", "ю", "Я", "я"};

    Text tempText;
    string forText;
    int countEn = 0;
    int countRu = 0;
    int countTotal = 0;
    int index;

    void Update()
    {
        clearDictionary = GameObject.FindGameObjectsWithTag("words");
    }

    //для рандомного вывода слов из словаря в тексты UI (кнопка "Rotate")
    public void loadRandomWord()
    {
        //открывает файл dictionaryEn.txt считывает все строки и передает их в массив dictEnglishMassive (по отдельнности)
        dictEnglishMassive = File.ReadAllLines(Application.persistentDataPath + "/dictionaryEn.txt");
        dictRussianMassive = File.ReadAllLines(Application.persistentDataPath + "/dictionaryRus.txt");

        //выводит рандомную строку из массива dictEnglishMassive в текст enTextOutput.text
        for (int i = 0; i < dictEnglishMassive.Length; i++)
        {
            enTextOutput.text = dictEnglishMassive[Random.Range(0, dictEnglishMassive.Length)];
        }

        //брёт индекс (номер) рандомной строки выведенной в текст enTextOutput.text из массива dictEnglishMassive
        index = System.Array.IndexOf(dictEnglishMassive, enTextOutput.text);

        //выводит строку с тем-же индексом, что и в массиве dictEnglishMassive, в текст ruTextOutput.text из массива dictRussianMassive
        for (int i = 0; i < dictRussianMassive.Length; i++)
        {
            ruTextOutput.text = dictRussianMassive[index];
        }
    }

    //для добавления новых слов в словари и массивы (кнопка ОК)
    public void addAndSaveWord()
    {
        enTextInput = engWordField.text;
        ruTextInput = ruWordField.text;

        //открывает файл dictionaryEn.txt считывает все строки и передает их в массив dictEnglishMassive (по отдельнности)
        dictEnglishMassive = File.ReadAllLines(Application.persistentDataPath + "/dictionaryEn.txt");
        dictRussianMassive = File.ReadAllLines(Application.persistentDataPath + "/dictionaryRus.txt");

        /* функция проверки "пустые поля для заполнения или нет"(если пустые то  warning.text = "Заполните оба поля").
          Так-же проверяет, присутствуют русские симолы в английском
          поле или нет (если присутствуют, то warning.text = "Только английские символы").
          Присутствуют английские симолы в русском поле или нет (если присутствуют, то warning.text = "Только русские символы").
          Если-же все условия соблюдены правильно, то warning.text = "" и функция работает дальше */
        for (int i = 0; i < engMassive.Length; i++)
        {
            for (int k = 0; k < ruMassive.Length; k++)
            {
                if (enTextInput == "" || ruTextInput == "")
                {
                    warningText.text = "Заполните оба поля";
                }
                else
                {
                    if (ruTextInput != "" || enTextInput != "")
                    {
                        if (ruTextInput.Contains(engMassive[i]))
                        {
                            warningText.text = "Только русские символы";
                        }
                        if (enTextInput.Contains(ruMassive[k]))
                        {
                            warningText.text = "Только английские символы";
                        }
                        else
                        {
                            if (ruTextInput != "" || enTextInput != "")
                            {
                                if (enTextInput.Contains(engMassive[i]) && ruTextInput.Contains(ruMassive[k]))
                                {
                                    warningText.text = "";

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
                if (a.Contains(enTextInput)) //если string a содержит хоть одну такую-же запись как в List<string> engWord
                {

                    if (enTextInput.Length > 0) //если длинна string eng больше ноля
                    {
                        warningText.text = "Это слово есть в словаре";
                        enTextInput = "";
                    }
                }
            }
            for (int i = 0; i < dictEnglishMassive.Length; i++) //этот цикл проверяет файл английского словаря на повторы 
                                                                //(если слова в файле английского словаря поторяются, 
                                                                //то warning.text = "Это слово есть в словаре")
            {
                if (enTextInput == dictEnglishMassive[i])
                {
                    countEn = 1;
                    warningText.text = "Это слово есть в словаре";
                }

                StartCoroutine(wait());
                IEnumerator wait()
                {
                    yield return new WaitForSeconds(0.1f);
                    countEn = 0;
                }
            }
            if (countEn == 0 && enTextInput.Length > 0)
            {
                engWord.Add(enTextInput);

                //функция добавления английских слов
                File.AppendAllText(Application.persistentDataPath + "/dictionaryEn.txt", enTextInput + "\r\n");

                engWordField.text = "";
                warningText.text = "";
            }
           
            //для русского языка
            foreach (string b in ruWord) //этот цикл проверяет лист ruWord на повторы 
                                         //(если слова в engWord поторяются, то  warning.text = "Это слово есть в словаре")
            {
                if (b.Contains(ruTextInput)) //если string b содержит хоть одну такую-же запись как в List<string> ruWord
                {
                    if (ruTextInput.Length > 0) //если длинна string ru больше ноля
                    {
                        warningText.text = "Это слово есть в словаре";
                        ruTextInput = "";
                    }
                }
            }
            for (int i = 0; i < dictRussianMassive.Length; i++) //этот цикл проверяет файл русского словаря на повторы 
                                                                //(если слова в файле русского словаря поторяются, 
                                                                //то warning.text = "Это слово есть в словаре")
            {
                if (ruTextInput == dictRussianMassive[i])
                {
                    countRu = 1;
                    warningText.text = "Это слово есть в словаре";
                }

                StartCoroutine(wait1());
                IEnumerator wait1()
                {
                    yield return new WaitForSeconds(0.1f);
                    countRu = 0;
                }
            }
            if (countRu == 0 && ruTextInput.Length > 0)
            {
                ruWord.Add(ruTextInput);

                //функция добавления русских слов
                File.AppendAllText(Application.persistentDataPath + "/dictionaryRus.txt", ruTextInput + "\r\n");

                ruWordField.text = "";
                warningText.text = "";
            }
        }
    }

    //для создания массива словаря (конпка "Словарь")
    public void createListWord()
    {
        StartCoroutine(wait());
        IEnumerator wait()
        {
            yield return new WaitForSeconds(0.1f);

            string[] dictE = File.ReadAllLines(Application.persistentDataPath + "/dictionaryEn.txt"); //массив который берётся из текстового файла
            string[] dictR = File.ReadAllLines(Application.persistentDataPath + "/dictionaryRus.txt"); //массив который берётся из текстового файла

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

    //для очистки InputField (кнопка "Закрыть" на панели добавить слова)
    public void clearInputFields()
    {
        engWordField.text = "";
        ruWordField.text = "";
    }

    //для очистки массива словаря (кнопка "Закрыть" на панели полного словаря)
    public void clearListWord()
    {
        for (int i = 0; i < clearDictionary.Length; i++)
        {
            Destroy(clearDictionary[i]);
        }
    }

    public void clearDict()
    {
        File.WriteAllText(Application.persistentDataPath + "/dictionaryEn.txt", text);
        File.WriteAllText(Application.persistentDataPath + "/dictionaryRus.txt", text);
        enTextOutput.text = "";
        ruTextOutput.text = "";
    }

    public void clearStatusBar()
    {
        warningText.text = "";
    }
}
