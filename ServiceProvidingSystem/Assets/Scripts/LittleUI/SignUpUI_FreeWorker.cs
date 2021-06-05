using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignUpUI_FreeWorker : MonoBehaviour
{
    public InputField Input_Account;
    public InputField Input_Password;
    public InputField Input_Password2;
    public InputField Input_Tag;
    public InputField Input_Phone;
    public Text SignUpMessage;

    //注册点击事件
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
        if (Input_Tag.text.Length <= 0)
        {
            SignUpMessage.text = "技术类型不能为空！！";
            return;
        }
        if (!DataManager.CheckPhone(Input_Phone.text))
        {
            SignUpMessage.text = "请填写正确手机号！！";
            return;
        }

        string message;
        if (DataManager.GetInstance().SignUp_FreeWorker(Input_Account.text, Input_Password.text, Input_Tag.text, Input_Phone.text, out message))
        {
            SignUpMessage.text = Input_Account.text + "自由职业者注册成功";
        }
        else
        {
            SignUpMessage.text = message;
        }

    }

    public void Click_Signin()
    {
        //SystemManager.GetInstance().ToSignIn();
    }
    //退出事件
    public void Click_Exit()
    {
        SystemManager.GetInstance().Close();
    }
}
