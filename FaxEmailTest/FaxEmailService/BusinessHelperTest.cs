using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FaxAndEmailService;
using FaxEmailTest.Common;
using FaxAndEmailService.Common;

namespace FaxEmailTest.FaxEmailService
{
    [TestFixture]
    class BusinessHelperTest
    {
        [SetUp]
        public void Init()
        {
            Config.ConfigDatabase();
        }

        [Test]
        public void GetSendType_AlertOptionTest()
        {
            LocalAccount account = new LocalAccount();
            account.IsConfigByV3 = false;
            account.AlertOption = AlertOptionEnum.Fax;
            Guid accountId = new Guid("4C5E9CC7-02A8-44B8-9520-81C84DC0A2C3");
            BusinessType type = BusinessType.RiskLevelChanged;
            BusinessParameter parameter = new BusinessParameter(account, null, accountId, type);
            SendType result = BusinessHelper.GetSendType(parameter);
            Assert.AreEqual(SendType.Email, result);
        }

        [Test]
        public void GetSendType_OrderTest()
        {
            LocalAccount account = new LocalAccount();
            account.IsConfigByV3 = true;

            Guid accountId = new Guid("4C5E9CC7-02A8-44B8-9520-81C84DC0A2C3");

            BusinessType type = BusinessType.OrderExecuted;

            BusinessParameter parameter = new BusinessParameter(account, null, accountId, type);
            SendType result = BusinessHelper.GetSendType(parameter);
            Assert.AreEqual(SendType.None, result,"when orderstate not assign shoud be none");

            parameter = new BusinessParameter(account, null, accountId, type, FaxAndEmail.Repository.Model.OrderState.Deleted);
            result = BusinessHelper.GetSendType(parameter);
            Assert.AreEqual(SendType.Fax, result,"shoud from v3 deleteOrderSendType");

            parameter = new BusinessParameter(account, null, accountId, type, FaxAndEmail.Repository.Model.OrderState.Executed);
            result = BusinessHelper.GetSendType(parameter);
            Assert.AreEqual(SendType.Fax, result,"shoud from v3 ExecutedOrderSendType");

            account.IsConfigByV3 = false;
            account.OrderOption = OrderConfirmationEnum.None;

            parameter = new BusinessParameter(account, null, accountId, type);
            result = BusinessHelper.GetSendType(parameter);
            Assert.AreEqual(SendType.None, result,"when not configed by v3 ,shoud be the same with account.OrderOption");
        }


        [Test]
        public void GetSendType_BalanceTest()
        {
            LocalAccount account = new LocalAccount();
            account.IsConfigByV3 = true;
            Guid accountId = new Guid("4C5E9CC7-02A8-44B8-9520-81C84DC0A2C3");
            BusinessType type = BusinessType.Balance;
            BusinessParameter parameter = new BusinessParameter(account, null, accountId, type);
            SendType result = BusinessHelper.GetSendType(parameter);
            Assert.AreEqual(SendType.Fax, result,"When config by v3");

            account.IsConfigByV3 = false;
            account.BalanceOption = BalanceOptionEnum.None;
            parameter = new BusinessParameter(account, null, accountId, type);
            result = BusinessHelper.GetSendType(parameter);
            Assert.AreEqual(SendType.None, result,"When not config by v3");
        }



        [Test]
        public void GetSendType_PasswordTest()
        {
            LocalCustomer customer = new LocalCustomer();
            customer.IsConfigByV3 = true;
            //Guid accountId = new Guid("4C5E9CC7-02A8-44B8-9520-81C84DC0A2C3");
            BusinessType type = BusinessType.PasswordReset;
            //BusinessParameter parameter = new BusinessParameter(account, null, accountId, type);
            //SendType result = BusinessHelper.GetSendType(parameter);
            //Assert.AreEqual(SendType.Fax, result,"When config by v3");
            customer.PwdOption = PasswordOptionEnum.Email;

            BusinessParameter parameter = new BusinessParameter(null, customer, Guid.Empty, type);
            SendType result = BusinessHelper.GetSendType(parameter);
            Assert.AreEqual(SendType.EmailAndSMS, result,"When not config by v3");
        }



    }
}
