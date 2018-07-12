﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MySQL数据库操作
{
    class Program
    {
        static void Main(string[] args)
        {
            //sql连接字符串
            string sqlConStr = "Database = sikiedu;Data Source = 127.0.0.1;port = 3306;user id= root;Password = 123456";
            MySqlConnection sqlConnection = new MySqlConnection(sqlConStr);

            sqlConnection.Open();//打开连接

            #region 数据库查询
            //MySqlCommand sqlCmd = new MySqlCommand("select * from users", sqlConnection);
            //MySqlDataReader reader = sqlCmd.ExecuteReader();
            //if (reader.HasRows)
            //{
            //    while (reader.Read())
            //    {
            //        string username = reader.GetString("username");
            //        string passward = reader.GetString("passward");
            //        Console.WriteLine(username + "  :  " + passward);
            //    }
            //}
            ////关闭读取流以及连接
            //reader.Close();
            #endregion

            #region 数据库插入
            string username = "ddd";
            string passward = "444";
            string cmdTxt = "insert into users set username ='" + username + "',password='" + passward + "'";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandText = cmdTxt;
            cmd.ExecuteNonQuery();
            #endregion

            sqlConnection.Close();

            Console.ReadKey();
        }
    }
}
