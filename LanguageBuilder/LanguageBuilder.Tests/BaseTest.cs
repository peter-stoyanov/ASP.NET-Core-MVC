using AutoFixture;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LanguageBuilder.Tests
{
    public abstract class BaseTest
    {
        protected Fixture _fixture;

        public BaseTest()
        {
            _fixture = new Fixture();
        }

        protected void CompareDates(DateTime expected, DateTime actual)
        {
            Assert.Equal(expected.Year, actual.Year);
            Assert.Equal(expected.Month, actual.Month);
            Assert.Equal(expected.Day, actual.Day);
        }

        protected void CompareDatesExact(DateTime expected, DateTime actual)
        {
            this.CompareDates(expected, actual);
            Assert.Equal(expected.Hour, actual.Hour);
            Assert.Equal(expected.Minute, actual.Minute);
        }
    }
}
