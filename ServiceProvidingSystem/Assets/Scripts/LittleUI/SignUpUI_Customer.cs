using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignUpUI_Customer : MonoBehaviour
{
    public InputField Input_Account;
    public InputField Input_Password;
    public InputField Input_Password2;
    public Text SignUpMessage;

    public void Click_Signup()
    {
        if (Input_Account.text.Length <= 0)
        {
            SignUpMessage.text = "账号不能为空！！";
            return;
        }
        if (Input_Password.text.Length <= 0)
        {
            SignUpMessage.text = "密码不能为空！！";
            return;
        }
        if (Input_Password2.text != Input_Password.text)
        {
            SignUpMessage.text = "两次输入密码不相同！！";
            return;
        }

        string message;
        if (DataManager.GetInstance().SignUp_Customer(Input_Account.text, Input_Password.text, out message))
        {
            SignUpMessage.text = Input_Account.text + "客户注册成功";
        }
        else
        {
            SignUpMessage.text = message;
        }

    }

    public void Click_Signin()
    {
        SystemManager.GetInstance().ToSignIn();
    }

    public void Click_Exit()
    {
        SystemManager.GetInstance().Close();
    }
}
