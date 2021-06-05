using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;

public class Search_FreeWorkerUI : MonoBehaviour
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
            datas = DataManager.GetInstance().FindWorkerByID(searchInfo.text);
        else if (searchType.value == 1)
            datas = DataManager.GetInstance().FindWorkerByName(searchInfo.text);
        else
            datas = DataManager.GetInstance().FindWorkerByTag(searchInfo.text);

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
            string no = DataManager.GetTrueString(datas.Tables[0].Rows[i]["FWno"] as string);
            string name = DataManager.GetTrueString(datas.Tables[0].Rows[i]["FWname"] as string);
            string tag = DataManager.GetTrueString(datas.Tables[0].Rows[i]["FWtag"] as string);
            string phone = DataManager.GetTrueString(datas.Tables[0].Rows[i]["FWphone"] as string);
            string email = DataManager.GetTrueString(datas.Tables[0].Rows[i]["FWemail"] as string);
            string qq = DataManager.GetTrueString(datas.Tables[0].Rows[i]["FWqq"] as string);

            obj.GetComponent<WorkerData>().SetData(new List<string>{ no, name, tag, phone, email, qq});
            scrtr.sizeDelta = new Vector2(scrtr.sizeDelta.x, scrtr.sizeDelta.y + 10 + obj.GetComponent<RectTransform>().sizeDelta.y);
            obj.transform.SetParent(scrtr);
        }
    }
}
