using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using iExchange.Common;
using System.Diagnostics;
using System.Net.Sockets;

namespace JavaTraderTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TcpClient client = new TcpClient("do3.iexchange.bz", 8000);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
