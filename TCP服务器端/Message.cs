using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP服务器端
{
    class Message
    {
        private const int DATA_MAX_LENGTH = 1024;
        private byte[] data = new byte[DATA_MAX_LENGTH];
        private int startIndex = 0;//存取了多少个字节的数据在数组里面
        public byte[] Data
        {
            get { return data; }
        }

        public int StartIndex
        {
            get { return startIndex; }
        }
        
        public int RemainSize
        {
            get { return data.Length - startIndex; }
        }

        public void AddCount(int count)
        {
            startIndex += count;
        }

        public void ReadMessage()
        {
            while(true)
            {
                if (startIndex <= 4)
                    return;

                int count = BitConverter.ToInt32(data, 0);
                if ((startIndex - 4) >= count)
                {
                    string s = Encoding.UTF8.GetString(data, 4, count);
                    Console.WriteLine("解析数据：" + s);
                    Array.Copy(data, count + 4, data, 0, startIndex - count - 4);
                    startIndex -= (count + 4);
                }
                else
                {
                    break;
                }
            }
        }


    }


}
