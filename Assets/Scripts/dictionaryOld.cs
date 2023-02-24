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

    string[] ruMassive = {"�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�",
    "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�",
    "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�",
    "�", "�", "�", "�", "�", "�"};

    void Start()
    {
        StartCoroutine(LoadText("dictionaryEn.txt", "dictionaryRus.txt"));

        //Debug.Log(Application.persistentDataPath);

        //string[] dictE = File.ReadAllLines(Application.dataPath + "/Standard Assets/dictionaryEn.txt"); //������ ������� ������ �� ���������� �����
        //string[] dictR = File.ReadAllLines(Application.dataPath + "/Standard Assets/dictionaryRus.txt"); //������ ������� ������ �� ���������� �����


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
        //TextAsset textAssetDictEnglish = Resources.Load<TextAsset>("dictionaryEn"); //��������� dictionaryEn.txt �� ����� Resources
        //                                                                            //(�������� � windows � � android)

        //toMassiveEn = textAssetDictEnglish.ToString();                              //string toMassive ��������� TextAsset textAsset

        //dictEnglish = toMassiveEn.Split(separators, System.StringSplitOptions.RemoveEmptyEntries); //��������� string toMassive �� ������,
        //                                                                                           //� ��� ������ ��������� string[] dictEnglish

        ////������ ���� �����, ��� � 3 ������ ����
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

    //��� �������� ������ "dictionaryEn.txt", "dictionaryRus.txt"
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
    
    //��� ���������� � ���� "dictionaryRus.txt"
    //IEnumerator saveRuText(string fileName)
    //{
    //    string path = Application.streamingAssetsPath + "/" + fileName;

    //    if (Application.isMobilePlatform)
    //    {
    //        UnityWebRequest webRequest = UnityWebRequest.Get(path);

    //        yield return webRequest.SendWebRequest();

    //        rusWordTextOut.text = webRequest.uploadHandler.contentType;

    //        //����� �������� string path � ������ � ���� ������ ������������ � ������� ��� ������ 
    //    }
    //    else
    //    {
    //        File.AppendAllText(path, "\r\n");
    //        File.AppendAllText(path, ru);
    //    }
    //}

    //��� ���������� � ���� "dictionaryEn.txt"
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

    //��� ���������� ����� ���� � ������� � ������� (������ ��)
    public void addWords()
    {
        ru = ruWordField.text;
        eng = engWordField.text;

        //string dictEn = Application.dataPath + "/Standard Assets/dictionaryEn.txt"; //���� � ����� �������
        //string dictRus = Application.dataPath + "/Standard Assets/dictionaryRus.txt"; //���� � ����� �������

        //string[] dictE = File.ReadAllLines(Application.dataPath + "/Standard Assets/dictionaryEn.txt"); //������ ������� ������ �� ���������� �����
        //string[] dictR = File.ReadAllLines(Application.dataPath + "/Standard Assets/dictionaryRus.txt"); //������ ������� ������ �� ���������� �����


        StartCoroutine(LoadText("dictionaryEn.txt", "dictionaryRus.txt"));

        /* ������� �������� "������ ���� ��� ���������� ��� ���"(���� ������ ��  warning.text = "��������� ��� ����").
           ���-�� ���������, ������������ ������� ������ � ����������
           ���� ��� ��� (���� ������������, �� warning.text = "������ ���������� �������").
           ������������ ���������� ������ � ������� ���� ��� ��� (���� ������������, �� warning.text = "������ ������� �������").
           ����-�� ��� ������� ��������� ���������, �� warning.text = "" � ������� �������� ������ */
        for (int i = 0; i < engMassive.Length; i++)
        {
            for (int k = 0; k < ruMassive.Length; k++)
            {
                if (eng == "" || ru == "")
                {
                    warning.text = "��������� ��� ����";
                }
                else
                {
                    if (ru != "" || eng != "")
                    {
                        if (ru.Contains(engMassive[i]))
                        {
                            warning.text = "������ ������� �������";
                        }
                        if (eng.Contains(ruMassive[k]))
                        {
                            warning.text = "������ ���������� �������";
                        }
                        else
                        {
                            if (ru != "" || eng != "")
                            {
                                if (eng.Contains(engMassive[i]) && ru.Contains(ruMassive[k]))
                                {
                                    warning.text = "";

                                    countTotal = 1; //��������� ��������� �������

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

        /* ������� ���������� ����� ���� � ����� �������� � ������� �������� ���� 
           (���� ����� ��� ���� � ������ �������, ���� � �������� �������, �� �����
           �� ����������� �� � ����� �������, �� � ������� ����) */
        if (countTotal == 1)
        {
            //��� ����������� �����
            foreach (string a in engWord) //���� ���� ��������� ���� engWord �� ������� 
                                          //(���� ����� � engWord ����������, ��  warning.text = "��� ����� ���� � �������")
            {
                if (a.Contains(eng)) //���� string a �������� ���� ���� �����-�� ������ ��� � List<string> engWord
                {

                    if (eng.Length > 0) //���� ������ string eng ������ ����
                    {
                        warning.text = "��� ����� ���� � �������";
                        eng = "";
                    }
                }     
            }
            for (int i = 0; i < dictEnglishMassive.Length; i++) //���� ���� ��������� ���� ����������� ������� �� ������� 
                                                   //(���� ����� � ����� ����������� ������� ����������, 
                                                   //�� warning.text = "��� ����� ���� � �������")
            {
                if (eng == dictEnglishMassive[i])
                {
                    countEn = 1;
                    warning.text = "��� ����� ���� � �������";
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

                //����� ��� ������� ���������� ����

                engWordField.text = "";
                warning.text = "";
            }
            //��� �������� �����
            foreach (string b in ruWord) //���� ���� ��������� ���� ruWord �� ������� 
                                         //(���� ����� � engWord ����������, ��  warning.text = "��� ����� ���� � �������")
            {
                if (b.Contains(ru)) //���� string b �������� ���� ���� �����-�� ������ ��� � List<string> ruWord
                {
                    if (ru.Length > 0) //���� ������ string ru ������ ����
                    {
                        warning.text = "��� ����� ���� � �������";
                        ru = "";
                    }
                }
            }
            for (int i = 0; i < dictRussianMassive.Length; i++) //���� ���� ��������� ���� �������� ������� �� ������� 
                                                   //(���� ����� � ����� �������� ������� ����������, 
                                                   //�� warning.text = "��� ����� ���� � �������")
            {
                if (ru == dictRussianMassive[i])
                {
                    countRu = 1;
                    warning.text = "��� ����� ���� � �������";
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

                //����� ��� ������� ���������� ����

                ruWordField.text = "";
                warning.text = "";
            }
        }

      

        //if (eng == "" || ru == "")
        //{
        //    warning.text = "��������� ��� ����";
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
        //            warning.text = "������ ������� �������";
        //        }
        //    }
        //}

        //for (int i = 0; i < ruMassive.Length; i++)
        //{
        //    if (eng != "")
        //    {
        //        if (eng.Contains(ruMassive[i]))
        //        {
        //            warning.text = "������ ���������� �������";
        //        }
        //    }
        //}
    }

    
    
    //��� �������� ������� ������� (������ "�������")
    public void createListWord()
    {
        StartCoroutine(wait());
        IEnumerator wait()
        {
            yield return new WaitForSeconds(0.1f);

            string[] dictE = File.ReadAllLines(Application.dataPath + "/Standard Assets/dictionaryEn.txt"); //������ ������� ������ �� ���������� �����
            string[] dictR = File.ReadAllLines(Application.dataPath + "/Standard Assets/dictionaryRus.txt"); //������ ������� ������ �� ���������� �����

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

    //��� ������� ������� ������� (������ "�������" �� ������ ������� �������)
    public void clearListWord()
    {
        for (int i = 0; i < clearDictionary.Length; i++)
        {
            Destroy(clearDictionary[i]);
        }  
    }

    //��� ���������� ������ ���� �� ������� � ������ UI (������ "Rotate")
    public void outputWords()
    {
        //string[] dictE = File.ReadAllLines(Application.dataPath + "/Standard Assets/dictionaryEn.txt"); //������ ������� ������ �� ���������� �����
        //string[] dictR = File.ReadAllLines(Application.dataPath + "/Standard Assets/dictionaryRus.txt"); //������ ������� ������ �� ���������� �����

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
        TextAsset textAssetDictEnglish = Resources.Load<TextAsset>("dictionaryEn"); //��������� dictionaryEn.txt �� ����� Resources
                                                                                    //(�������� � windows � � android)

        toEn = textAssetDictEnglish.ToString();                              //string toMassive ��������� TextAsset textAsset

        dictEnglishMassive = toEn.Split(separators, System.StringSplitOptions.RemoveEmptyEntries); //��������� string toMassive �� ������,
                                                                                                   //� ��� ������ ��������� string[] dictEnglish

        //������ ���� �����, ��� � 3 ������ ����
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

    //��� ������� InputField (������ "�������" �� ������ �������� �����)
    public void clearInputFields()
    {
        engWordField.text = "";
        ruWordField.text = "";
    }
}