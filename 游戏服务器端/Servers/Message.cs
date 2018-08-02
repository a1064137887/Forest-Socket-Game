using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace GameServer.Servers
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


        public void ReadMessage(int newDataAmount,Action<RequestCode,ActionCode,string> processDataCallBack)
        {
            startIndex += newDataAmount;
            while(true)
            {
                //最小数据长度大于4，前4位为数据头
                if (startIndex <= 4)
                    return;

                int count = BitConverter.ToInt32(data, 0);
                if ((startIndex - 4) >= count)//如果当前消息是完整的
                {
                    RequestCode requestCode = (RequestCode)BitConverter.ToInt32(data, 4);//请求协议
                    ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 8);
                    string s = Encoding.UTF8.GetString(data, 12, count - 8);
                    Console.WriteLine("解析数据：" + s);
                    processDataCallBack(requestCode, actionCode, s);
                    Array.Copy(data, count + 4, data, 0, startIndex - count - 4);
                    startIndex -= (count + 4);
                }
                else
                {
                    break;
                }
            }
        }


        public static byte[] PackData(ActionCode actionCode,string data)
        {
            byte[] requestCodeBytes = BitConverter.GetBytes((int)actionCode);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            int dataAmount = requestCodeBytes.Length + dataBytes.Length;
            byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
            return dataAmountBytes.Concat(requestCodeBytes).ToArray<byte>().Concat(dataBytes).ToArray<byte>();
        }


    //class end
    }
//namespace end
}
