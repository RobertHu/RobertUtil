using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FaxAndEmailService.Services;
using FaxAndEmail.Repository.Model;

namespace FaxEmailTest.FaxEmailService
{
    [TestFixture]
    public class ProcessNewAccountServiceTest
    {

        [SetUp]
        public void Init()
        {
           // FaxEmailTest.Common.Config.ConfigPhysicalPath();
            FaxEmailTest.Common.Config.ConfigDatabase();
            FaxEmailTest.Common.Config.ConfigXmlDataSettings();
        }

        [Test]
        public void ProcessTest()
        {
            NewAccountNotifycation notifycation = new NewAccountNotifycation(Guid.Parse("4C5E9CC7-02A8-44B8-9520-81C84DC0A2C3"), 2, null);
            ProcessNewAccountService.Default.Process(notifycation);
        }

    }
}
