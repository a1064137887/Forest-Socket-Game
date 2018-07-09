using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace TCP服务器端
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddress = IPAddress.Parse("192.168.0.101");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 7800);
            serverSocket.Bind(ipEndPoint);//绑定IP和端口号
            serverSocket.Listen(0);//开始监听端口号，0代表排队数量不限制
            Socket clientSocket = serverSocket.Accept();//接收一个客户端

            //向客户端发送一条消息
            string msg = "Hello Client ! 你好客户端";
            byte[] data = System.Text.Encoding.UTF8.GetBytes(msg);
            clientSocket.Send(data);

            //接收客户端一条消息
            byte[] dataBuffer = new byte[1024];
            int dataLen = clientSocket.Receive(dataBuffer);
            string msgReceive = System.Text.Encoding.UTF8.GetString(dataBuffer,0,dataLen);
            Console.WriteLine(msgReceive);

            Console.ReadKey();
            clientSocket.Close();//断开与客户端的连接，并不是关闭客户端
            serverSocket.Close();//关闭自身的连接

        }
    }
}