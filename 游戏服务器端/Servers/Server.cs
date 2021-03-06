﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using GameServer.Controller;
using Common;

namespace GameServer.Servers
{
    class Server
    {
        private Socket serverSocket;
        private IPEndPoint ipEndPoint;
        private List<Client> clientList = new List<Client>();
        private ControllerManager controllerManager;

        public Server() { }
        public Server(string ip,int port)
        {
            ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            controllerManager = new ControllerManager(this);
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
            client.Start();//客户端打开监听
            Console.WriteLine("接收到一个客户端的连接，目前clientList中有" +clientList.Count+"个客户端");
            serverSocket.BeginAccept(AcceptCallBack, null);
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

        /// <summary>
        /// 在处理完客户端的请求之后发送响应
        /// </summary>
        /// <param name="client">接收响应的客户端</param>
        /// <param name="actionCode">处理</param>
        /// <param name="data">数据</param>
        public void SendResponse(Client client,ActionCode actionCode,string data)
        {
            client.Send(actionCode, data);
        }

        public void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data, Client client)
        {
            //这样通过 Server 作为中介让 Client 调用 controllerManager ，降低耦合性
            controllerManager.HandleRequest(requestCode, actionCode, data, client);
        }

    //class end
    }

//namespace end
}