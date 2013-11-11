using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FaxAndEmail.Repository;
using FaxAndEmail.Repository.Sql;
using FaxEmailTest.Common;
using System.Diagnostics;

namespace FaxEmailTest
{
    [TestFixture]
    public class V3AccountRepositoryTest
    {
        [SetUp]
        public void Initialize()
        {
            Config.ConfigDatabase();
        }
        [Test]
        public void GetNewAccountRecordTest()
        {
            Guid accountID = new Guid("4C5E9CC7-02A8-44B8-9520-81C84DC0A2C3");
            var newAccountInfo = V3AccountRepository.GetNewAccountRecord(accountID);
            Debug.WriteLine("Begin");
            Assert.NotNull(newAccountInfo);
            Assert.AreEqual(newAccountInfo.AccountCode, "tzz001648");
            Assert.AreEqual(newAccountInfo.CustomerCode, "tzz001648");
            Assert.AreEqual(newAccountInfo.LoginName, "tzz001648");
        }
    }
}
