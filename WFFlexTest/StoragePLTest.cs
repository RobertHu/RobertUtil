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
    public class StoragePLTest
    {
        private OtherPlDataProducer _producer = new OtherPlDataProducer(BusinessTypeEnum.StoragePL);
        private int _recordIndex = 1;

        [Test]
        public void ExpenseTest()
        {
            var storageData = _producer.GetExpenseData();
            bool result = FileExportHelper.Export(storageData, "storage_expense.txt", _recordIndex, OtherPLManager.Process);
            Assert.IsTrue(result);
        }

        [Test]
        public void IncomeTest()
        {
            var storageData = _producer.GetIncomeData();
            bool result = FileExportHelper.Export(storageData, "storage_income.txt", _recordIndex, OtherPLManager.Process);
            Assert.IsTrue(result);
        }
    }
}
