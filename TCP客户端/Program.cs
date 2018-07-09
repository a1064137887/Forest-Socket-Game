using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace TCP客户端
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse("192.168.0.101"), 7800));

            byte[] data = new byte[1024];
            int count = clientSocket.Receive(data);
            string msg = Encoding.UTF8.GetString(data);
            Console.WriteLine(msg);

            while(true)
            {
                string str = Console.ReadLine();
                clientSocket.Send(Encoding.UTF8.GetBytes(str));
            }


            Console.ReadKey();
            clientSocket.Close();
        }
    }
}
