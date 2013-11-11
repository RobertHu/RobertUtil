using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace HignPerformanceSocketTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "hign performance socket";
            byte[] data = UTF8Encoding.Default.GetBytes(str);
            TcpClient client = new TcpClient("10.2.10.1", 999);
            NetworkStream stream = client.GetStream();
            byte[] readBuf = new byte[1024];
            //stream.BeginRead(readBuf, 0, readBuf.Length, ar =>
            //{
            //    var len = stream.EndRead(ar);
            //    string receiveStr = UTF8Encoding.Default.GetString(readBuf, 0, len);
            //    Console.WriteLine(receiveStr);
            //}, null);

            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    stream.Write(data, 0, data.Length);
                    Thread.Sleep(1000);
                }
            });
            //thread.IsBackground = true;
            //thread.Start();

            stream.Write(data, 0, data.Length);

            Console.Read();
            
        }
    }
}
