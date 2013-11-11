using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FaxAndEmail.Repository;
using FaxAndEmail.Repository.Util;
using FaxAndEmailService.Common;
using FaxAndEmailService.Services;
using FaxAndEmail.Helper;
using System.IO;
namespace FaxEmailTest.Common
{
    public static class Config
    {
        public static void ConfigDatabase()
        {
            Settings.ConnectionString = "data source=ws0303;initial catalog=iExchange_V3;user id=sa;password=Omni1234;Connect Timeout=300";
            Settings.LocalConnectionString = "data source=ws0303;initial catalog=Fax;user id=sa;password=Omni1234;Connect Timeout=300";
            SessionManager.InitializeLocalSessionFactory(Settings.LocalConnectionString);
            SessionManager.InitializeV3SessionFactory(Settings.ConnectionString);
        }

        //public static void ConfigPhysicalPath()
        //{
        //    string path = @"D:\Teams\iExchangeCollection\iExchange3 Team\iExchange3\FaxAndEmailService";
        //    PathUtil.SetPhysicalPath(path);
        //}

        public static void ConfigXmlDataSettings()
        {
            SMSServer.SMSManager.Default.Initialize(Path.Combine(PathUtil.GetPhysicalPath(), "Setting.xml"));
            var orgCodes = new List<string>();
            string defaultOrg=  StringConstants.DEFAULTTEMPLATE;
            orgCodes.Add(defaultOrg);
            var generalDataPath = new List<string>();
            var smsDataPath = new List<string>();
            generalDataPath.Add(FormatPath(defaultOrg,PathUtil.GetDataPath(defaultOrg)));
            smsDataPath.Add(FormatPath(defaultOrg,PathUtil.GetSMSDataPath(defaultOrg)));
            ProcessNewAccountService.Default.InnerService.Initialize(PathUtil.MacroPath, generalDataPath, smsDataPath);
            ProcessPasswordResetService.Default.InnerService.Initialize(PathUtil.MacroPath, generalDataPath, smsDataPath);
        }
        private static string FormatPath(string org, string path)
        {
            return string.Format("{0}|{1}", org, path);
        }
    }
}
