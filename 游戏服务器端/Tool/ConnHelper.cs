using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GameServer.Tool
{
    class ConnHelper
    {
        
        public const string CONNECTSTR = "datasource=127.0.0.1;port=3306;database=sikiedu;user=root;pwd=root";

        public static MySqlConnection Connect()
        {
            MySqlConnection conn = new MySqlConnection(CONNECTSTR);
            try
            {
                conn.Open();
                return conn;
            }
            catch(Exception e)
            {
                Console.WriteLine("连接数据库出现异常 ： " + e);
                return null;
            }
        }

        public static void CloseConnection(MySqlConnection connection)
        {
            if (connection == null)
            {
                Console.WriteLine("SQL连接为空");
                return;
            }
            connection.Close();
        }

    }
}
