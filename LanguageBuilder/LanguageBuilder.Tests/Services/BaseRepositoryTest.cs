using LanguageBuilder.Data;
using LanguageBuilder.Data.Models;
using LanguageBuilder.Data.Services;
using LanguageBuilder.Services.Implementations;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using FluentAssertions;
using LanguageBuilder.Tests.Services.TestDoubles;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoFixture.Xunit2;

namespace LanguageBuilder.Tests.Services
{
    public class BaseRepositoryTest : BaseServiceTest
    {
        public BaseRepositoryTest()
        { }

        [Theory, AutoData]
        public void Add_PersistEntityInDb_WhenEntityIsValid(string wordcontent)
        {
            // Arange
            var sut = new WordRepositoryTestDouble(InMemoryDatabase);

            sut.Add(new Word { Content = wordcontent });

            // Act
            var result = sut.GetAll().FirstOrDefault();

            // Assert

            Assert.NotNull(result);
            result.Id.Should().Equals(1);
            result.Content.Should().Equals(wordcontent);
        }

        [Theory, AutoData]
        public void Add_ThrowsException_WhenEntityIsNotValid(string wordcontent)
        {
            // Arange
            var sut = new WordRepositoryTestDouble(InMemoryDatabase);

            // Act
            var exception = Record.Exception(() => sut.Add(new Word()));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ValidationException>(exception);
        }

        [Theory, AutoData]
        public async Task AddAsync_PersistEntityInDb_WhenEntityIsValid(string wordcontent)
        {
            // Arange
            var sut = new WordRepositoryTestDouble(InMemoryDatabase);

            await sut.AddAsync(new Word { Content = wordcontent });

            // Act
            var result = sut.GetAll().FirstOrDefault();

            // Assert

            Assert.NotNull(result);
            result.Id.Should().Equals(1);
            result.Content.Should().Equals(wordcontent);
        }

        [Fact]
        public async Task AddAsync_ThrowsException_WhenEntityIsNotValid()
        {
            // Arange
            var sut = new WordRepositoryTestDouble(InMemoryDatabase);

            // Act
            var exceptionAsync = Record.ExceptionAsync(() => sut.AddAsync(new Word()));

            // Assert
            if (exceptionAsync != null)
            {
                var exception = await exceptionAsync;
                Assert.IsType<ValidationException>(exception);
            }
        }

        [Theory, AutoData]
        public void Exist_ReturnsTrue_WhenEntityInDb(string wordcontent)
        {
            // Arange
            int id = 1;
            var sut = new WordRepositoryTestDouble(InMemoryDatabase);

            sut.Add(new Word { Content = wordcontent });

            // Act
            var result = sut.Exist(id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Exist_ReturnsFalse_WhenEntityNotInDb()
        {
            // Arange
            int id = 1;
            var sut = new WordRepositoryTestDouble(InMemoryDatabase);

            // Act
            var result = sut.Exist(id);

            // Assert
            Assert.False(result);
        }

        [Theory, AutoData]
        public void Delete_ReturnsDeletedEntity_WhenValidId(string wordcontent)
        {
            // Arange
            int id = 1;
            var sut = new WordRepositoryTestDouble(InMemoryDatabase);
            sut.Add(new Word { Content = wordcontent });

            // Act
            var result = sut.Delete(id);
            var count = sut.GetAll().Count;

            // Assert
            Assert.NotNull(result);
            Assert.True(count == 0);
            result.Id.Should().Equals(1);
            result.Content.Should().Equals(wordcontent);
        }

        [Theory, AutoData]
        public async Task DeleteAsync_ReturnsDeletedEntity_WhenValidId(string wordcontent)
        {
            // Arange
            int id = 1;
            var sut = new WordRepositoryTestDouble(InMemoryDatabase);
            sut.Add(new Word { Content = wordcontent });

            // Act
            var result = await sut.DeleteAsync(id);
            var count = sut.GetAll().Count;

            // Assert
            Assert.NotNull(result);
            Assert.True(count == 0);
            result.Id.Should().Equals(1);
            result.Content.Should().Equals(wordcontent);
        }

        [Theory, AutoData]
        public void Delete_ReturnsNull_WhenNotValidId(string wordContent)
        {
            // Arange
            int id = 2;
            var sut = new WordRepositoryTestDouble(InMemoryDatabase);
            sut.Add(new Word { Content = wordContent });

            // Act
            var result = sut.Delete(id);
            var count = sut.GetAll().Count;

            // Assert
            Assert.Null(result);
            Assert.True(count == 1);
        }

        [Theory, AutoData]
        public async Task DeleteAsync_ReturnsNull_WhenNotValidId(string wordContent)
        {
            // Arange
            int id = 2;
            var sut = new WordRepositoryTestDouble(InMemoryDatabase);
            sut.Add(new Word { Content = wordContent });

            // Act
            var result = await sut.DeleteAsync(id);
            var count = sut.GetAll().Count;

            // Assert
            Assert.Null(result);
            Assert.True(count == 1);
        }

        [Theory, AutoData]
        public void GetAll_ReturnsAllEntities(string wordContent1, string wordContent2, string wordContent3)
        {
            // Arange
            SeedDbWithTestData(wordContent1, wordContent2, wordContent3, out WordRepositoryTestDouble sut, out Word word1, out Word word2, out Word word3);

            // Act
            var result = sut.GetAll().ToArray();

            // Assert
            Assert.NotNull(result);
            result[0].Should().BeEquivalentTo(word1);
            result[1].Should().BeEquivalentTo(word2);
            result[2].Should().BeEquivalentTo(word3);
        }

        private void SeedDbWithTestData(string wordContent1, string wordContent2, string wordContent3, out WordRepositoryTestDouble sut, out Word word1, out Word word2, out Word word3)
        {
            sut = new WordRepositoryTestDouble(InMemoryDatabase);
            word1 = new Word { Content = wordContent1 };
            word2 = new Word { Content = wordContent2 };
            word3 = new Word { Content = wordContent3 };
            sut.Add(word1);
            sut.Add(word2);
            sut.Add(word3);
        }

        [Theory, AutoData]
        public async Task GetAllAsync_ReturnsAllEntities(string wordContent1, string wordContent2, string wordContent3)
        {
            // Arange
            SeedDbWithTestData(wordContent1, wordContent2, wordContent3, out WordRepositoryTestDouble sut, out Word word1, out Word word2, out Word word3);

            // Act
            var result = (await sut.GetAllAsync()).ToArray();

            // Assert
            Assert.NotNull(result);
            result[0].Should().BeEquivalentTo(word1);
            result[1].Should().BeEquivalentTo(word2);
            result[2].Should().BeEquivalentTo(word3);
        }

        [Theory, AutoData]
        public void GetAllByIds_ReturnsCorrectEntities(string wordContent1, string wordContent2, string wordContent3)
        {
            // Arange
            SeedDbWithTestData(wordContent1, wordContent2, wordContent3, out WordRepositoryTestDouble sut, out Word word1, out Word word2, out Word word3);

            // Act
            var result = sut.GetAll(new int[] { 2, 3 }).ToArray();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Length == 2);
            result[0].Should().BeEquivalentTo(word2);
            result[1].Should().BeEquivalentTo(word3);
        }

        [Theory, AutoData]
        public async Task GetAllAsyncByIds_ReturnsCorrectEntities(string wordContent1, string wordContent2, string wordContent3)
        {
            // Arange
            SeedDbWithTestData(wordContent1, wordContent2, wordContent3, out WordRepositoryTestDouble sut, out Word word1, out Word word2, out Word word3);

            // Act
            var result = (await sut.GetAllAsync(new int[] { 2, 3 })).ToArray();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Length == 2);
            result[0].Should().BeEquivalentTo(word2);
            result[1].Should().BeEquivalentTo(word3);
        }

        [Theory, AutoData]
        public async Task GetAsyncById_ReturnsEntity_IfValidId(string wordContent1, string wordContent2, string wordContent3)
        {
            // Arange
            SeedDbWithTestData(wordContent1, wordContent2, wordContent3, out WordRepositoryTestDouble sut, out Word word1, out Word word2, out Word word3);

            // Act
            var result = await sut.GetAsync(2);
            
            // Assert
            Assert.NotNull(result);
            result.Should().BeEquivalentTo(word2);
        }

        [Theory, AutoData]
        public async Task GetAsyncById_ReturnsNull_IfNotValidId(string wordContent1, string wordContent2, string wordContent3)
        {
            // Arange
            SeedDbWithTestData(wordContent1, wordContent2, wordContent3, out WordRepositoryTestDouble sut, out Word word1, out Word word2, out Word word3);

            // Act
            var result = await sut.GetAsync(222);

            // Assert
            Assert.Null(result);
        }
    }
}

