using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlexInterface.Common;

namespace WFFlexTest.Util
{
    public class OtherPlDataProducer
    {
        private BusinessTypeEnum _businessType;
        public OtherPlDataProducer(BusinessTypeEnum businessType)
        {
            this._businessType = businessType;
        }

        public InterestStorageLevyCommisionPLData GetExpenseData()
        {
            var interestData = GetPLData();
            interestData.OriginAmount = Math.Abs(interestData.OriginAmount);
            return interestData;
        }

        public InterestStorageLevyCommisionPLData GetIncomeData()
        {
            var interestData = GetPLData();
            interestData.OriginAmount = interestData.OriginAmount > 0 ? -interestData.OriginAmount : interestData.OriginAmount;
            return interestData;
        }

        private InterestStorageLevyCommisionPLData GetPLData()
        {
            InterestStorageLevyCommisionPLData interestData = new InterestStorageLevyCommisionPLData();
            interestData.Type = this._businessType;
            interestData.FromMt4LoginID = "person001";
            interestData.ToMt4LoginID = "WF0002";
            interestData.FromTradeDay = DateTime.Now.AddDays(-10);
            interestData.ToTradeDay = DateTime.Now;
            interestData.OriginAmount = 2345.5678m;
            interestData.ExchangeRate = 7.8889m;
            interestData.EquvAmount = interestData.OriginAmount * interestData.ExchangeRate;
            interestData.CurrencyCode = "HKD";
            interestData.FromCustomerNo = "MinCustomerNo";
            interestData.ToCustomerNo = "MaxCustomerNo";
            interestData.ProductName = "LLS伦敦金";
            return interestData;
        }
    }
}
