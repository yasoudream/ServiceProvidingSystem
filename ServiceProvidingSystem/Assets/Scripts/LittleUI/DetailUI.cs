using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DetailUI : MonoBehaviour
{
    public List<Text> detailTexts;
    
    public void Show(List<string> data)
    {
        for (int i = 0; i < detailTexts.Count; ++i)
            detailTexts[i].text = data[i];
    }
}
