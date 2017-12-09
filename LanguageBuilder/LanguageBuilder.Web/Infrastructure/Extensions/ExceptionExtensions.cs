using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageBuilder.Web.Infrastructure.Extensions
{
    public static class ExceptionExtensions
    {
        //private static ILog Log = LogManager.GetLogger("Pipeline");

        public static void SaveToLog(this Exception ex)
        {
          //  ExceptionExtensions.Log.Error(ex.Message, ex);
        }

        public static void SaveToLog(this Exception ex, string query)
        {
            string key = "query";

            log4net.ThreadContext.Properties[key] = query;

            //ExceptionExtensions.Log.Error(ex.Message, ex);

            log4net.ThreadContext.Properties[key] = null;
        }
    }
}
