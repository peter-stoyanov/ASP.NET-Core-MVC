using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;

namespace LanguageBuilder.Web.Infrastructure.Extensions
{
    public static class ExceptionExtensions
    {
        private static ILog Logger;

        static ExceptionExtensions()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());

            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            Logger = LogManager.GetLogger(typeof(Program));
        }

        public static void SaveToLog(this Exception ex)
        {
            ExceptionExtensions.Logger.Error(ex.Message, ex);
        }
    }
}
