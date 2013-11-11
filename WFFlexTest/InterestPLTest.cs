using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FlexInterface.Common;
using FlexInterface.Helper;
using System.IO;
using WFFlexTest.Util;
namespace WFFlexTest
{
    [TestFixture]
    public class InterestPLTest
    {
        private OtherPlDataProducer producer = new OtherPlDataProducer(BusinessTypeEnum.InterestPL);
        [Test]
        public void ExportExpenseTest()
        {
            var interestData = producer.GetExpenseData();
            int recordIndex = 1;
bool result =FileExportHelper.Export(interestData, "interest_expense.txt", recordIndex, OtherPLManager.Process);
Assert.IsTrue(result);
        }

        [Test]
        public void ExportIncomeTest()
        {
            var interestData = producer.GetIncomeData(); 
            int recordIndex = 1;
            bool result = FileExportHelper.Export(interestData, "interest_income.txt", recordIndex, OtherPLManager.Process);
            Assert.IsTrue(result);
        }
    }
}
