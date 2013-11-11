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
    public class LevyPLTest
    {
        private OtherPlDataProducer _producer = new OtherPlDataProducer(BusinessTypeEnum.Levy);
        private int _recordIndex = 1;

        [Test]
        public void IncomeTest()
        {
            var levyData = _producer.GetIncomeData();
            bool result = FileExportHelper.Export(levyData, "levy_income.txt", _recordIndex, OtherPLManager.Process);
            Assert.IsTrue(result);
        }

        [Test]
        public void ExpenseTest()
        {
            var levyData = _producer.GetExpenseData();
            bool result = FileExportHelper.Export(levyData, "levy_expense.txt", _recordIndex, OtherPLManager.Process);
            Assert.IsTrue(result);
            
        }
    }
}
