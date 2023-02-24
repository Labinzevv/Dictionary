using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class firstLaunch : MonoBehaviour // выплняет код при первом запуске однократно
{
    int firstRun = 0;
    string text = "";

    void Start()
    {
        firstRun = PlayerPrefs.GetInt("firstRun"); 

        if (firstRun == 0)
        {
            firstRun = 1;
            PlayerPrefs.SetInt("firstRun", firstRun);
            File.WriteAllText(Application.persistentDataPath + "/dictionaryEn.txt", text);
            File.WriteAllText(Application.persistentDataPath + "/dictionaryRus.txt", text);
        } 
    }
}
