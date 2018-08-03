using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using GameServer.Model;

namespace GameServer.DAO
{
    class UserDAO
    {

        /// <summary>
        /// 查询帐户
        /// </summary>
        /// <param name="mysqlConn">SQL连接</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public User VerifyUser(MySqlConnection mysqlConn,string username,string password)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = mysqlConn;
                cmd.CommandText = "select * from user where username = @username and password = @password";
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    User user = new User(id, username, password);
                    return user;
                }
                else
                {
                    return null;
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine("在 VerifyUser 时出现异常   " + ex);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return null;


        }

        public bool GetUserByUsername(MySqlConnection mysqlConn, string username)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = mysqlConn;
                cmd.CommandText = "select * from user where username = @username";
                cmd.Parameters.AddWithValue("username", username);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("在 GetUserByUsername 出现异常   " + ex);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return false;

        }


        public void AddUser(MySqlConnection mysqlConn,string username,string password)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "insert into user set username = @username , password = @password";
                cmd.Connection = mysqlConn;
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine("在 AddUser 出现异常   " + ex);
            }
        }



    }

}
