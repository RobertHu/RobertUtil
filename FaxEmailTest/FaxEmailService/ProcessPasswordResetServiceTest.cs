using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FaxAndEmailService;
using iExchange.Common;
using FaxAndEmailService.Services;

namespace FaxEmailTest.FaxEmailService
{
    [TestFixture]
    class ProcessPasswordResetServiceTest
    {

        [SetUp]
        public void Init()
        {
            FaxEmailTest.Common.Config.ConfigDatabase();
            FaxEmailTest.Common.Config.ConfigXmlDataSettings();
        }

        [Test]
        public void ProcessTest()
        {
            PasswordResetInfo info = new PasswordResetInfo(Guid.Parse("EF84F319-64CD-4540-893C-171104CF0804"), "nancy1", "12345678");
            ProcessPasswordResetService.Default.Process(info);
        }

    }
}
