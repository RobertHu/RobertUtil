using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FaxAndEmail.Repository.Util;
namespace FaxEmailTest.Account
{
    [TestFixture]
    public class LocalAccountUtilTest
    {
        [Test]
        public void GetMobileTest()
        {
            string mobile = "12534";
            string phone = "ddddd";
            string result = LocalAccountUtil.GetMobile(mobile, phone);
            Assert.AreEqual(mobile, result);
            mobile = null;
            result = LocalAccountUtil.GetMobile(mobile, phone);
            Assert.AreEqual(phone, result);
            phone = null;
            result = LocalAccountUtil.GetMobile(mobile, phone);
            Assert.AreEqual(string.Empty, result);
        }
    }
}
