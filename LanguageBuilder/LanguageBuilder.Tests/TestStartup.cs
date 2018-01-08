using AutoMapper;
using LanguageBuilder.Web.Infrastructure.Mapping;

namespace LanguageBuilder.Tests
{
    public class TestStartup
    {
        private static object sync = new object();
        private static bool testsInitialized = false;

        public static void Initialize()
        {
            lock (sync)
            {
                if (!testsInitialized)
                {
                    Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
                    testsInitialized = true;
                }
            }
        }
    }
}
