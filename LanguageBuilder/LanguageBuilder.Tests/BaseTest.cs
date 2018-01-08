using AutoFixture;

namespace LanguageBuilder.Tests
{
    public abstract class BaseTest
    {
        protected Fixture _fixture;

        public BaseTest()
        {
            _fixture = new Fixture();
        }
    }
}
