using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP客户端
{
    class Message
    {
        public static byte[] GetBytes(string _data)
        {
            byte[] data = Encoding.UTF8.GetBytes(_data);
            int len = data.Length;
            byte[] dataLen = BitConverter.GetBytes(len);
            byte[] newBytes = dataLen.Concat(data).ToArray();
            return newBytes;
        }
    }
}
