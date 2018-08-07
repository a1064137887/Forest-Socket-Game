using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Model;
using MySql.Data.MySqlClient;

namespace GameServer.DAO
{
    class ResultDAO
    {

        public Result GetResultByID(MySqlConnection conn,int userID)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select * from result where userid = @userid";
                cmd.Parameters.AddWithValue("userid", userID);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    int id = reader.GetInt32("id");
                    int totalCount = reader.GetInt32("totalcount");
                    int winCount = reader.GetInt32("wincount");
                    Result result = new Result(id, userID, totalCount, winCount);
                    return result;
                }
                else
                {
                    Result result = new Result(-1, userID, 0, 0);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("在 GetResultByID 出现异常   " + ex);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return null;

        }

    }
}
