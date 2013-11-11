using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FaxAndEmail.Repository;
using FaxEmailTest.Common;
using FaxAndEmail.Repository.Sql;
namespace FaxEmailTest.Account
{
    [TestFixture]
    class LocalAccountRepositoryTest
    {
        [SetUp]
        public void Init()
        {
            Config.ConfigDatabase();
        }

        [Test]
        public void AddAccountAndCustomerTest()
        {
            Guid accountiD = new Guid("4C5E9CC7-02A8-44B8-9520-81C84DC0A2C3");
            var newaccountInfo = V3AccountRepository.GetNewAccountRecord(accountiD);
            Assert.NotNull(newaccountInfo, "new account info is null");
            bool result = LocalAccountRepository.AddAccountAndCustomer(newaccountInfo);
            Assert.IsTrue(result, "add account and customer failed");
        }
    }
}
