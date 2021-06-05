using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;


public class Search_CustomerUI : MonoBehaviour
{
    public InputField searchInfo;
    public Dropdown searchType;
    public Text message;
    public DetailUI workerDetail;

    public GameObject scrollContent;
    public GameObject workerDataPrefab;

    public void Click_Search()
    {
        if (searchInfo.text.Length <= 0)
        {
            message.text = "请输入数据";
            return;
        }
        DataSet datas;
        if (searchType.value == 0)
            datas = DataManager.GetInstance().FindCustomerByID(searchInfo.text);
        else
            datas = DataManager.GetInstance().FindCustomerByName(searchInfo.text);

        //删除表上的物体
        int childCount = scrollContent.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            DestroyImmediate(scrollContent.transform.GetChild(0).gameObject);
        }
        RectTransform scrtr = scrollContent.GetComponent<RectTransform>();
        scrtr.sizeDelta = new Vector2(scrtr.sizeDelta.x, 10);

        if (!DataManager.CheckExists(datas))
        {
            message.text = "查无数据";
            return;
        }


        for (int i = 0; i < datas.Tables[0].Rows.Count; i++)
        {
            GameObject obj = Instantiate(workerDataPrefab);
            string no = DataManager.GetTrueString(datas.Tables[0].Rows[i]["CTno"] as string);
            string name = DataManager.GetTrueString(datas.Tables[0].Rows[i]["CTname"] as string);
            string tag = DataManager.GetTrueString(datas.Tables[0].Rows[i]["CTaddr"] as string);
            string phone = DataManager.GetTrueString(datas.Tables[0].Rows[i]["CTphone"] as string);
            string email = DataManager.GetTrueString(datas.Tables[0].Rows[i]["CTemail"] as string);
            string qq = DataManager.GetTrueString(datas.Tables[0].Rows[i]["CTqq"] as string);

            obj.GetComponent<CustomerData>().SetData(new List<string> { no, name, tag, phone, email, qq });
            scrtr.sizeDelta = new Vector2(scrtr.sizeDelta.x, scrtr.sizeDelta.y + 10 + obj.GetComponent<RectTransform>().sizeDelta.y);
            obj.transform.SetParent(scrtr);
        }
    }
}