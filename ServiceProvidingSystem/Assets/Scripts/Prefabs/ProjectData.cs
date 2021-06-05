using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectData : MonoBehaviour
{
    public List<string> m_data;
    public Text text;

    public void SetData(List<string> data)
    {
        m_data = new List<string>(data);
        text.text = "ID: " + data[1] + "  名称: " + data[2];
    }

    public void Click_Show()
    {
        SystemManager.GetInstance().ShowProjectDetail(m_data);
    }
}
