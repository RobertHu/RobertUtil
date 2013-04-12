using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using iExchange.Common;
using System.Diagnostics;

namespace JavaTraderTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "net.tcp://ws0210:9001/AsyncSslServer/Service/CommandCollectService";
            EndpointAddress address = new EndpointAddress(url);
            NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
            binding.MaxBufferPoolSize = binding.MaxReceivedMessageSize = binding.MaxBufferSize = 16 * 1024 * 1024;
            //binding.SendTimeout = TimeSpan.FromSeconds(3);
            binding.OpenTimeout = TimeSpan.FromSeconds(1);
            ChannelFactory<ICommandCollectService> factory = new ChannelFactory<ICommandCollectService>(binding, address);
            ICommandCollectService commandCollectService = factory.CreateChannel();
            Token token = new Token();
            QuoteCommand quoteCommand = new QuoteCommand();
            commandCollectService.AddCommand(token, quoteCommand);
            Console.WriteLine("ok");
            Console.Read();
        }
    }
}
