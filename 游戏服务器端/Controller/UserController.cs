using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Servers;
using GameServer.DAO;
using GameServer.Model;

namespace GameServer.Controller
{
    class UserController : BaseController
    {
        private UserDAO userDAO = new UserDAO();
        private ResultDAO resultDAO = new ResultDAO();
        public UserController()
        {
            requestCode = RequestCode.User;
        }

        public string Login(string data, Client client ,Server server)
        {
            string[] strs = data.Split(',');
            string username = strs[0];
            string password = strs[1];
            User user = userDAO.VerifyUser(client.SqlConn, username, password);

            if(user == null)
            {
                return ((int)ReturnCode.Fail).ToString();
            }
            else
            {
                
                Result result = resultDAO.GetResultByID(client.SqlConn, user.Id);
                //登录成功后要返回战绩信息，字符串格式化组拼
                return string.Format("{0},{1},{2},{3}", ((int)ReturnCode.Success).ToString(), user.Username, result.totalCount, result.winCount);
            }

        }

        public string Register(string data,Client client,Server server)
        {
            string[] strs = data.Split(',');
            string username = strs[0];
            string password = strs[1];
            bool isHave = userDAO.GetUserByUsername(client.SqlConn, username);
            if(isHave)//如果账号存在了
            {
                return ((int)ReturnCode.Fail).ToString();
            }
            //不存在的话就添加到数据库中
            userDAO.AddUser(client.SqlConn, username, password);
            return ((int)ReturnCode.Success).ToString();
        }


    }


}
