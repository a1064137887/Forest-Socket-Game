using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace TCP客户端
{
    class ClientProgram
    {
        static void Main(string[] args)
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse("192.168.0.101"), 7800));

            byte[] data = new byte[1024];
            int count = clientSocket.Receive(data);
            string msg = Encoding.UTF8.GetString(data);
            Console.WriteLine(msg);

            for(int i = 0; i < 100; i++)
            {
                clientSocket.Send(Message.GetBytes(i.ToString()));
            }

            while (true)
            {
                string str = Console.ReadLine();
                if (str == "c")
                {
                    clientSocket.Close();
                    return;
                }
                clientSocket.Send(Message.GetBytes(str));
            }

        }


    }


}
