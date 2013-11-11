using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ServiceModel;
using JavaTraderTest;
using iExchange.Common;

namespace WcfIISTest
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
        }
    }
}
