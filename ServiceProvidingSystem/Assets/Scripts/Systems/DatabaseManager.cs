using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UnityEngine;
using System;

public class DatabaseManager
{
    //单例模式
    private static DatabaseManager instance;
    public static DatabaseManager GetInstance()
    {
        if (instance == null)
            instance = new DatabaseManager();
        return instance;
    }


    //连接字符串
    private static readonly string connStr = "Data Source=127.0.0.1,1433;" +
    "Initial Catalog=ServiceProvidingData;" +
    "User Id=zhangweijing;" +
    "Password=123456;" +
    "Integrated Security=false;";

    public bool isConnectSuccess = false;       //连接状态
    private SqlConnection sqlConnection;        //连接对象

    /// <summary>
    /// 连接数据库
    /// </summary>
    /// <returns></returns>
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
            Debug.LogError(ex.ToString());
            return false;
        }
        return true;
    }

    public void Exit()
    {
        if (isConnectSuccess)
            sqlConnection.Close();
        isConnectSuccess = false;
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
            Debug.LogError(ex.ToString());
            return false;
        }
        return true;
    }


}
