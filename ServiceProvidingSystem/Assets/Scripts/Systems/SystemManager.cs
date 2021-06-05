using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public DetailUI WorkerDetail;

    public string curAccount;
    public UserType curUserType;

    //单例
    private static SystemManager instance;
    public static SystemManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    //public void SigninSuccess()
    //{
    //    switch (curUserType)
    //    {
    //        case UserType.FreeWorker:
    //            mainUIobj_FreeWorker.SetActive(true);
    //            break;
    //        case UserType.Customer:
    //            mainUIobj_Customer.SetActive(true);
    //            break;
    //        case UserType.Manager:
    //            mainUIobj_Manager.SetActive(true);
    //            break;
    //    }
    //    signInUIobj.SetActive(false);
    //}

    //public void ToSignUp_FreeWorker()
    //{
    //    signInUIobj.SetActive(false);
    //    signUpUIobj_FreeWorker.SetActive(true);
    //}

    //public void ToSignUp_Customer()
    //{
    //    signInUIobj.SetActive(false);
    //    signUpUIobj_Customer.SetActive(true);
    //}

    //public void ToSignIn()
    //{
    //    signInUIobj.SetActive(true);
    //    signUpUIobj_Customer.SetActive(false);
    //    signUpUIobj_FreeWorker.SetActive(false);
    //}

    public void ShowWorkerDetail(List<string> data)
    {
        WorkerDetail.Show(data);
    }
    public void Close()
    {
        Application.Quit();
    }

    public static UserType ToUserType(int index)
    {
        if (index == 0)
            return UserType.FreeWorker;
        if (index == 1)
            return UserType.Customer;
        return UserType.Manager;
    }

}
