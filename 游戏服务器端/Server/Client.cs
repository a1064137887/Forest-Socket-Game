using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace GameServer.Server
{
    class Client
    {
        private Socket clientSocket;
        private Server server;
        private Message message;
        public Client() { }
        public Client(Socket clientSocket,Server server)
        {
            this.clientSocket = clientSocket;
            this.server = server;
        }

        public void Start()
        {
            clientSocket.BeginReceive(message.Data, message.StartIndex, message.RemainSize, SocketFlags.None, ReceiveCallBack, null);
        }

        public void Close()
        {
            if (clientSocket == null) return;
            clientSocket.Close();
            //从Server类中的List中移除Client对象
            server.RemoveClient(this);
        }

        private void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                int count = clientSocket.EndReceive(ar);
                if(count == 0)
                {
                    Close();
                }

                //TODO：处理数据
                Start();//重新打开监听
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                Close();
            }
        }
 

    //class end
    }

//namespace end
}
