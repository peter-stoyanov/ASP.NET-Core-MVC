using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;

namespace LanguageBuilder.Web.Infrastructure.Extensions
{
    public static class ExceptionExtensions
    {
        private static ILog logger;

        static ExceptionExtensions()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());

            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            logger = LogManager.GetLogger(typeof(Program));
        }

        public static void SaveToLog(this Exception ex)
        {
            ExceptionExtensions.logger.Error(ex.Message, ex);
        }

        public static void SaveToLog(this Exception ex, string query)
        {
            string key = "query";

            log4net.ThreadContext.Properties[key] = query;

            ExceptionExtensions.logger.Error(ex.Message, ex);

            log4net.ThreadContext.Properties[key] = null;
        }
    }
}
