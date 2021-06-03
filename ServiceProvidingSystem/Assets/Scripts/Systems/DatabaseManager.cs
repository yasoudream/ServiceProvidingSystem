using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System;

public class DatabaseManager
{
    private static DatabaseManager instance;
    public static DatabaseManager GetInstance()
    {
        return instance;
    }

    //懒汉单例
    static DatabaseManager()
    {
        if (instance == null)
            instance = new DatabaseManager();
    }

    //连接字符串
    private static readonly string connStr = "Data Source=127.0.0.1,1433;" +
    "Initial Catalog=ServiceProvidingData;" +
    "User Id=zhangweijing;" +
    "Password=123456;" +
    "Integrated Security=false;";

    public bool isConnectSuccess = false;       //连接状态
    private SqlConnection sqlConnection;        //连接对象

    public bool Connect()
    {
        sqlConnection = new SqlConnection(connStr);
        isConnectSuccess = false;
        try
        {
            sqlConnection.Open();
            isConnectSuccess = true;
            //DebugLog.instance.Log("succeed!");

        }
        catch (Exception ex)
        {
            //DebugLog.instance.Log("Failed");
            //DebugLog.instance.Log(ex.ToString());
            return false;
        }
        return true;
    }

    public void Exit()
    {
        if (isConnectSuccess)
            sqlConnection.Close();
    }

    public DataSet Select(string sqlcommand)
    {
        if (!isConnectSuccess)
            return new DataSet();
        //生成命令
        SqlCommand command = sqlConnection.CreateCommand();
        command.CommandType = System.Data.CommandType.Text;
        command.CommandText = sqlcommand;
        //创建适配器
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = command;
        DataSet ds = new DataSet();
        //执行语句
        da.Fill(ds);

        return ds;
    }

    /// <summary>
    /// insert等无返回语句
    /// </summary>
    /// <param name="sqlcommand"></param>
    public bool ExecuteNonQuery(string sqlcommand)
    {
        if (!isConnectSuccess)
            return false;
        //生成命令
        SqlCommand command = sqlConnection.CreateCommand();
        command.CommandType = System.Data.CommandType.Text;
        command.CommandText = sqlcommand;
        try
        {
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }

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
}
