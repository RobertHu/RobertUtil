using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FlexInterface.Common;
using FlexInterface.Helper;
using WFFlexTest.Util;
namespace WFFlexTest
{
    [TestFixture]
    public class CommissionPLTest
    {
        private OtherPlDataProducer producer = new OtherPlDataProducer(BusinessTypeEnum.Commission);
        private int recordIndex = 1;
        [Test]
        public void ExpenseTest()
        {
            var commisionData = producer.GetExpenseData();
            bool result = FileExportHelper.Export(commisionData, "commision_expense.txt", this.recordIndex, OtherPLManager.Process);
            Assert.IsTrue(result);
        }

        [Test]
        public void IncomeTest()
        {
            var commisionData = producer.GetIncomeData();
            bool result = FileExportHelper.Export(commisionData, "commsion_incomde.txt", this.recordIndex, OtherPLManager.Process);
            Assert.IsTrue(result);
        }
    }
}
