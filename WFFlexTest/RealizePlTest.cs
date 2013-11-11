using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FlexInterface.Helper;
using FlexInterface.Common;
using WFFlexTest.Util;
namespace WFFlexTest
{
    [TestFixture]
    public class RealizePlTest
    {
        [Test]
        public void ExportTest()
        {
            RealizedPLData plData = new RealizedPLData();
            plData.Type = BusinessTypeEnum.RealizedPL;
            plData.FromMt4LoginID = "person001";
            plData.ToMt4LoginID = "WF0002";
            plData.FromTradeDay = DateTime.Now.AddDays(-10);
            plData.ToTradeDay = DateTime.Now;
            plData.OriginAmount = 2345.5678m;
            plData.ExchangeRate = 7.8889m;
            plData.EquvAmount = plData.OriginAmount * plData.ExchangeRate;
            plData.CurrencyCode = "HKD";
            plData.MonthlyChangeRate = 0.8999m;
            plData.ProductName = "LLS伦敦银";
            int recordIndex = 1;
            bool result = FileExportHelper.Export(plData, "realizedPl.txt", recordIndex, RealizedPlManager.Process);
            Assert.IsTrue(result);
        }
    }
}
