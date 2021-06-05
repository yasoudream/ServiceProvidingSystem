using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignInUI : MonoBehaviour
{
    public InputField Input_Account;
    public InputField Input_Password;
    public Dropdown Input_UserType;
    public Text SignInMessage;

    //登陆按钮事件
    public void Click_Signin()
    {
        //错误检测
        if (Input_Account.text.Length <= 0)
        {
            SignInMessage.text = "账号不能为空！！";
            return;
        }
        if (Input_Password.text.Length <= 0)
        {
            SignInMessage.text = "密码不能为空！！";
            return;
        }

        string message;
        if (DataManager.GetInstance().SignIn(Input_Account.text, Input_Password.text, SystemManager.ToUserType(Input_UserType.value), out message))
        {
            SystemManager.GetInstance().SigninSuccess();
        }
        else
        {
            SignInMessage.text = message;
        }
    }

    public void Click_SignUp_FreeWorker()
    {
        SystemManager.GetInstance().ToSignUp_FreeWorker();
    }

    public void Click_SignUp_Customer()
    {
        SystemManager.GetInstance().ToSignUp_Customer();
    }

    public void Click_Quit()
    {
        SystemManager.GetInstance().Close();
    }
}
