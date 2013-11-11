using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FlexInterface.Common;
using FlexInterface.Helper;

namespace FlexUnitTest
{
    [TestFixture]
    public class AdjustmentTest
    {
        private AdjustmentData _adjustment;
        [SetUp]
        public void SetUpTest()
        {
            _adjustment = new AdjustmentData()
            {
                Mt4LoginId="111",
                AccountingMappingCode="333",
                CustomerCode="xiaoming",
                CustomerName="小名",
                SourceCurrencyName="HKD",
                ExchangeRate=7.345m,
                OriginAmount= 89.33m,
                TransactionNo="AdjustmentA_B_C",
                AccountingCurrencyName="USD",
                UpdateTime=DateTime.Now.AddDays(23)
            };
        }

        [Test]
        public void AddDescTest()
        {
            DepositInnerService.AddDesc(PaymentType.Adjustment, _adjustment);
            Assert.IsNotNull(_adjustment.Desc5);
            Assert.IsNotNull(_adjustment.Desc6);
            Assert.IsEmpty(_adjustment.Desc7);
            Assert.IsNotNull(_adjustment.Desc8);
        }
        [Test]
        public void ExportTest()
        {
            DepositInnerService.AddDesc(PaymentType.Adjustment, _adjustment);
            FlexPersistence.PersiteDesposit(new PaymentData[] { _adjustment }, null, 0, PaymentType.Adjustment);
        }
    }
}
