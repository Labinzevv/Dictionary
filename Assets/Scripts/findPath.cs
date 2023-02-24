using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class findPath : MonoBehaviour
{
    string m_Path;
    public Text path;

    void Update()
    {
        m_Path = Application.dataPath;
        path.text = m_Path;
    }
}
