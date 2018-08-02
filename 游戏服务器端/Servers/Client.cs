using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using Common;
using MySql.Data.MySqlClient;
using GameServer.Tool;

namespace GameServer.Servers
{
    class Client
    {
        private Socket clientSocket;
        private Server server;
        private Message message;
        private MySqlConnection sqlConn;
        public MySqlConnection SqlConn
        {
            get { return sqlConn; }
        }
        public Client() { }
        public Client(Socket clientSocket,Server server)
        {
            this.clientSocket = clientSocket;
            this.server = server;
            sqlConn = ConnHelper.Connect();
            message = new Message();
        }

        public void Start()
        {
            clientSocket.BeginReceive(message.Data, message.StartIndex, message.RemainSize, SocketFlags.None, ReceiveCallBack, null);
        }

        public void Close()
        {
            ConnHelper.CloseConnection(sqlConn);
            if (clientSocket == null) return;
            clientSocket.Close();
            //从Server类中的List中移除Client对象
            server.RemoveClient(this);
        }

        //接收消息的回调
        private void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                int count = clientSocket.EndReceive(ar);
                if(count == 0)
                {
                    Close();
                }
                Console.WriteLine("接收到客户端的数据");
                message.ReadMessage(count,OnProcessMessage);
                Start();//重新打开监听
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                Close();
            }
        }

        //接收消息的回调
        private void OnProcessMessage(RequestCode requestCode,ActionCode actionCode,string data)
        {
            server.HandleRequest(requestCode, actionCode, data, this);
        }

        public void Send(ActionCode actionCode,string data)
        {
            byte[] bytes = Message.PackData(actionCode, data);
            clientSocket.Send(bytes);
        }
 

    //class end
    }
//namespace end
}
