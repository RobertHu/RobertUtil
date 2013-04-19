using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using iExchange.Common;
using System.Diagnostics;
using System.Net.Sockets;
using System.IO;

namespace JavaTraderTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string path = @"D:\Work\javaTrader\lines.txt";
                string target = ".*?(xmlNode2.get_Item[(]\".*\"[)].*";
                FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
                stream.Close();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
