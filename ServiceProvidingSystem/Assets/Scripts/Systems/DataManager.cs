using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;

public enum UserType
{
    FreeWorker,Customer,Manager
}

public class DataManager : MonoBehaviour
{
    //单例
    private static DataManager instance;
    private DatabaseManager m_databaseManager;
    public static DataManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        m_databaseManager = DatabaseManager.GetInstance();
        if (!m_databaseManager.Connect())
            Debug.LogError("Connect Error");
    }

    public bool Connect()
    {
        if (!m_databaseManager.isConnectSuccess)
        {
            if (!m_databaseManager.Connect())
            {
                return false;
            }
        }
        return true;
    }

    public bool SignIn(string account, string password, UserType usertype, out string message)
    {
        message = string.Empty;
        if (!Connect())
        {
            message = "数据库连接失败";
            return false;
        }
        
        string command = string.Empty;
        switch(usertype)
        {
            case UserType.FreeWorker:
                command = "select * from FreeWorker where FWno = '" + account + "' and FWpassword = '" + password + "';";
                break;
            case UserType.Customer:
                command = "select * from Customer where CTno = '" + account + "' and CTpassword = '" + password + "';";
                break;
            case UserType.Manager:
                command = "select * from Manager where MNno = '" + account + "' and MNpassword = '" + password + "';";
                break;
        }

        if(CheckExists(m_databaseManager.Select(command)))
        {
            SystemManager.GetInstance().curAccount = account;
            SystemManager.GetInstance().curUserType = usertype;
            return true;
        }

        message = "登陆失败，请检查账号密码";
        return false;
        
    }

    public bool SignUp_FreeWorker(string account, string password, string tag, string FWphone, out string message)
    {
        message = string.Empty;
        if (!Connect())
        {
            message = "数据库连接失败";
            return false;
        }

        //用户名检查
        string command = "select * from FreeWorker where FWno = '" + account + "';";
        if(CheckExists(m_databaseManager.Select(command)))
        {
            message = "账号已存在";
            return false;
        }

        command = "insert into FreeWorker values('" + account + "','" + password + "','" + tag + "','" + FWphone + "');";
        if (m_databaseManager.ExecuteNonQuery(command))
        {
            return true;
        }
        message = "注册失败";
        return false;

    }

    public bool SignUp_Customer(string account, string password, out string message)
    {
        message = string.Empty;
        if (!Connect())
        {
            message = "数据库连接失败";
            return false;
        }

        //用户名检查
        string command = "select * from Customer where CTno = '" + account + "';";
        if (CheckExists(m_databaseManager.Select(command)))
        {
            message = "账号已存在";
            return false;
        }

        command = "insert into Customer values('" + account + "','" + password + "');";
        if (m_databaseManager.ExecuteNonQuery(command))
        {
            return true;
        }
        message = "注册失败";
        return false;

    }

    public DataSet FindWorkerByID(string id)
    {
        return m_databaseManager.Select("exec FindWorkerByID '" + id + "';");
    }

    public DataSet FindWorkerByName(string name)
    {
        return m_databaseManager.Select("exec FindWorkerByName '" + name + "';");
    }

    public DataSet FindWorkerByTag(string tag)
    {
        return m_databaseManager.Select("exec FindWorkerByTag '" + tag + "';");
    }

    public DataSet FindCustomerByID(string id)
    {
        return m_databaseManager.Select("exec FindCustomerByID '" + id + "';");
    }

    public DataSet FindCustomerByName(string name)
    {
        return m_databaseManager.Select("exec FindCustomerByName '" + name + "';");
    }

    public DataSet FindProjectByID(string id)
    {
        return m_databaseManager.Select("exec FindProjectByID '" + id + "';");
    }
    public DataSet FindProjectByName(string name)
    {
        return m_databaseManager.Select("exec FindProjectByName '" + name + "';");
    }
    public DataSet FindProjectByCTno(string ctno)
    {
        return m_databaseManager.Select("exec FindProjectByCTno '" + ctno + "';");
    }

    public DataSet FindProjectByFWno(string fwno)
    {
        return m_databaseManager.Select("exec FindProjectByFWno '" + fwno + "';");
    }

    public DataSet FindProjectByTag(string tag)
    {
        return m_databaseManager.Select("exec FindProjectByTag '" + tag + "';");
    }

    public void Close()
    {
        if (m_databaseManager.isConnectSuccess)
        {
            m_databaseManager.Exit();
        }
    }

    #region TOOLS
    /// <summary>
    /// 获得正确字符串
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static string GetTrueString(string data)
    {
        int index = -1;
        index = data.IndexOf(' ');
        if (index < 0)
            return data;
        return data.Substring(0, index);
    }


    /// <summary>
    /// Dataset是否为空
    /// </summary>
    /// <param name="dataset"></param>
    /// <returns></returns>
    public static bool CheckExists(DataSet dataset)
    {
        return dataset.Tables[0].Rows.Count > 0;
    }

    /// <summary>
    /// 检查手机号合理性
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public static bool CheckPhone(string num)
    {
        if (num.Length != 11)
            return false;
        foreach (char c in num)
        {
            if (c > '9' || c < '0')
                return false;
        }
        return true;
    }

    #endregion TOOLS

}
