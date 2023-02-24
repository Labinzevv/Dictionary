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

    //��� ����������� �������������� �������� � �������� ��� ����������� �����
    string[] engMassive = {"A", "a", "B", "b", "C", "c", "D", "d", "E", "e", "F", "f", "G", "g", "H", "h",
    "I", "i", "J", "j", "K", "k", "L", "l", "M", "m", "N", "n", "O", "o", "P", "p", "Q", "q", "R", "r", "S", "s",
    "T", "t", "U", "u", "V", "v", "W", "w", "X", "x", "Y", "y", "Z", "z"};
    string[] ruMassive = {"�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�",
    "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�",
    "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�",
    "�", "�", "�", "�", "�", "�"};

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

    //��� ���������� ������ ���� �� ������� � ������ UI (������ "Rotate")
    public void loadRandomWord()
    {
        //��������� ���� dictionaryEn.txt ��������� ��� ������ � �������� �� � ������ dictEnglishMassive (�� ������������)
        dictEnglishMassive = File.ReadAllLines(Application.persistentDataPath + "/dictionaryEn.txt");
        dictRussianMassive = File.ReadAllLines(Application.persistentDataPath + "/dictionaryRus.txt");

        //������� ��������� ������ �� ������� dictEnglishMassive � ����� enTextOutput.text
        for (int i = 0; i < dictEnglishMassive.Length; i++)
        {
            enTextOutput.text = dictEnglishMassive[Random.Range(0, dictEnglishMassive.Length)];
        }

        //��� ������ (�����) ��������� ������ ���������� � ����� enTextOutput.text �� ������� dictEnglishMassive
        index = System.Array.IndexOf(dictEnglishMassive, enTextOutput.text);

        //������� ������ � ���-�� ��������, ��� � � ������� dictEnglishMassive, � ����� ruTextOutput.text �� ������� dictRussianMassive
        for (int i = 0; i < dictRussianMassive.Length; i++)
        {
            ruTextOutput.text = dictRussianMassive[index];
        }
    }

    //��� ���������� ����� ���� � ������� � ������� (������ ��)
    public void addAndSaveWord()
    {
        enTextInput = engWordField.text;
        ruTextInput = ruWordField.text;

        //��������� ���� dictionaryEn.txt ��������� ��� ������ � �������� �� � ������ dictEnglishMassive (�� ������������)
        dictEnglishMassive = File.ReadAllLines(Application.persistentDataPath + "/dictionaryEn.txt");
        dictRussianMassive = File.ReadAllLines(Application.persistentDataPath + "/dictionaryRus.txt");

        /* ������� �������� "������ ���� ��� ���������� ��� ���"(���� ������ ��  warning.text = "��������� ��� ����").
          ���-�� ���������, ������������ ������� ������ � ����������
          ���� ��� ��� (���� ������������, �� warning.text = "������ ���������� �������").
          ������������ ���������� ������ � ������� ���� ��� ��� (���� ������������, �� warning.text = "������ ������� �������").
          ����-�� ��� ������� ��������� ���������, �� warning.text = "" � ������� �������� ������ */
        for (int i = 0; i < engMassive.Length; i++)
        {
            for (int k = 0; k < ruMassive.Length; k++)
            {
                if (enTextInput == "" || ruTextInput == "")
                {
                    warningText.text = "��������� ��� ����";
                }
                else
                {
                    if (ruTextInput != "" || enTextInput != "")
                    {
                        if (ruTextInput.Contains(engMassive[i]))
                        {
                            warningText.text = "������ ������� �������";
                        }
                        if (enTextInput.Contains(ruMassive[k]))
                        {
                            warningText.text = "������ ���������� �������";
                        }
                        else
                        {
                            if (ruTextInput != "" || enTextInput != "")
                            {
                                if (enTextInput.Contains(engMassive[i]) && ruTextInput.Contains(ruMassive[k]))
                                {
                                    warningText.text = "";

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
                if (a.Contains(enTextInput)) //���� string a �������� ���� ���� �����-�� ������ ��� � List<string> engWord
                {

                    if (enTextInput.Length > 0) //���� ������ string eng ������ ����
                    {
                        warningText.text = "��� ����� ���� � �������";
                        enTextInput = "";
                    }
                }
            }
            for (int i = 0; i < dictEnglishMassive.Length; i++) //���� ���� ��������� ���� ����������� ������� �� ������� 
                                                                //(���� ����� � ����� ����������� ������� ����������, 
                                                                //�� warning.text = "��� ����� ���� � �������")
            {
                if (enTextInput == dictEnglishMassive[i])
                {
                    countEn = 1;
                    warningText.text = "��� ����� ���� � �������";
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

                //������� ���������� ���������� ����
                File.AppendAllText(Application.persistentDataPath + "/dictionaryEn.txt", enTextInput + "\r\n");

                engWordField.text = "";
                warningText.text = "";
            }
           
            //��� �������� �����
            foreach (string b in ruWord) //���� ���� ��������� ���� ruWord �� ������� 
                                         //(���� ����� � engWord ����������, ��  warning.text = "��� ����� ���� � �������")
            {
                if (b.Contains(ruTextInput)) //���� string b �������� ���� ���� �����-�� ������ ��� � List<string> ruWord
                {
                    if (ruTextInput.Length > 0) //���� ������ string ru ������ ����
                    {
                        warningText.text = "��� ����� ���� � �������";
                        ruTextInput = "";
                    }
                }
            }
            for (int i = 0; i < dictRussianMassive.Length; i++) //���� ���� ��������� ���� �������� ������� �� ������� 
                                                                //(���� ����� � ����� �������� ������� ����������, 
                                                                //�� warning.text = "��� ����� ���� � �������")
            {
                if (ruTextInput == dictRussianMassive[i])
                {
                    countRu = 1;
                    warningText.text = "��� ����� ���� � �������";
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

                //������� ���������� ������� ����
                File.AppendAllText(Application.persistentDataPath + "/dictionaryRus.txt", ruTextInput + "\r\n");

                ruWordField.text = "";
                warningText.text = "";
            }
        }
    }

    //��� �������� ������� ������� (������ "�������")
    public void createListWord()
    {
        StartCoroutine(wait());
        IEnumerator wait()
        {
            yield return new WaitForSeconds(0.1f);

            string[] dictE = File.ReadAllLines(Application.persistentDataPath + "/dictionaryEn.txt"); //������ ������� ������ �� ���������� �����
            string[] dictR = File.ReadAllLines(Application.persistentDataPath + "/dictionaryRus.txt"); //������ ������� ������ �� ���������� �����

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

    //��� ������� InputField (������ "�������" �� ������ �������� �����)
    public void clearInputFields()
    {
        engWordField.text = "";
        ruWordField.text = "";
    }

    //��� ������� ������� ������� (������ "�������" �� ������ ������� �������)
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
