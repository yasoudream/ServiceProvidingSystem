using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerData : MonoBehaviour
{
    public List<string> m_data;
    public Text text;

    public void SetData(List<string> data)
    {
        m_data = new List<string>(data);
        text.text = "ID: " + data[0] + "  姓名: " + data[1];
    }

    public void Click_Show()
    {
        SystemManager.GetInstance().ShowCustomDetail(m_data);
    }
}
