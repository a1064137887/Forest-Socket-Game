using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace GameServer.Server
{
    class Server
    {
        private Socket serverSocket;
        private IPEndPoint ipEndPoint;
        private List<Client> clientList;

        public Server() { }
        public Server(string ip,int port)
        {
            ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        }

        //通过ip和port设置该类的ipEndPoint
        public void SetIpEndPoint(string ip,int port)
        {
            ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        }

        //开始连接
        public void Start()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(ipEndPoint);
            serverSocket.Listen(0);
            serverSocket.BeginAccept(AcceptCallBack,null);
        }
        
        private void AcceptCallBack(IAsyncResult ar)
        {
            Socket clientSocket = serverSocket.EndAccept(ar);
            Client client = new Client(clientSocket,this);
            clientList.Add(client);
        }

        public void RemoveClient(Client client)
        {
            if (client == null) return;
            //避免有多个Client同时调用该方法
            lock(clientList)
            {
                clientList.Remove(client);
            }
        }

    //class end
    }

//namespace end
}