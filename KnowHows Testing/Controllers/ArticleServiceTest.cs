using KnowHows_Back_End.Services;
using KnowHows_Back_End.Models;
using MongoDB.Driver;
using Moq;
using Xunit;
using Microsoft.Extensions.Options;
using FluentAssertions;
using AutoFixture;
using KnowHows_Back_End.Interfaces;
using KnowHows_Back_End.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace KnowHows_Testing.Controllers
{
    public class ArticleServiceTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IArticleService> _serviceMock;
        private readonly ArticleController _sut;

        public ArticleServiceTest()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IArticleService>>();
            _sut = new ArticleController(_serviceMock.Object);//creates the implementation in memory
        }

        [Fact]
        public async Task GetArticlesAsync_ShouldReturnOkResponse_WhenDataFound()
        {
            // Arrange
            var articleMock = _fixture.Create<List<Article>>();
            _serviceMock.Setup(x => x.GetArticlesAsync()).Returns(async () => articleMock);

            // Act
            var result = await _sut.Get().ConfigureAwait(false);

            // Assert
            Assert.NotNull(result);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<List<Article>>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value
                .Should()
                .NotBeNull()
                .And.BeOfType(articleMock.GetType());
            _serviceMock.Verify(x => x.GetArticlesAsync(), Times.Once());
        }

        [Fact]
        public async Task GetArticlesAsync_ShouldReturnNotFound_WhenDataNotFound()
        {
            //Arrange
            List<Article> response = null;
            _serviceMock.Setup(x => x.GetArticlesAsync()).Returns(async () => response);

            //Act
            var result = await _sut.Get().ConfigureAwait(false);

            //Assert
            Assert.NotNull(result);
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<NotFoundResult>();
            _serviceMock.Verify(x => x.GetArticlesAsync(), Times.Once());
        }

        [Fact]
        public async Task CreateArticleAsync_ShouldReturnOkResponse_WhenCreatedSuccesfully()
        {
            //Arrange
            var articleMock = _fixture.Create<Article>();
            _serviceMock.Setup(x => x.CreateArticleAsync(articleMock)).Returns(async () => articleMock);

            //Act
            var result = await _sut.Post(articleMock).ConfigureAwait(false);

            //Assert
            Assert.NotNull(result);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<CreatedAtActionResult>();
        }

        [Fact]
        public async Task CreateArticleAsync_ShouldReturnBadRequest_WhenInvalidData()
        {
            //Arrange
            var articleMock = _fixture.Create<Article>();
            articleMock.Title = null;
            _serviceMock.Setup(x => x.CreateArticleAsync(articleMock)).Returns(async () => articleMock);

            //Act
            var result = await _sut.Post(articleMock).ConfigureAwait(false);

            //Assert
            Assert.NotNull(articleMock);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
        }
    }
}