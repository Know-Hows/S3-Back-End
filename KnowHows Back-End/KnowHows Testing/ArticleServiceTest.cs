using KnowHows_Back_End.Services;
using KnowHows_Back_End.Models;
using MongoDB.Driver;
using Moq;
using Xunit;
using Microsoft.Extensions.Options;

namespace KnowHows_Testing
{
    public class ArticleServiceTest
    {
        [Fact]
        public async Task GetAsync_WithEmptyCollection_ReturnsEmptyList()
        {
            // Arrange
            var mockCollection = new Mock<IMongoCollection<Article>>();
            var mockOptions = new Mock<IOptions<KnowHowsDatabaseSettings>>();

            mockOptions.Setup(x => x.Value)
                       .Returns(new KnowHowsDatabaseSettings
                       {
                           ConnectionString = "ConnString",
                           DatabaseName = "DBName",
                           ArticleCollectionName = "ArticleCollName"
                       });

            var emptyCursor = new Mock<IAsyncCursor<Article>>();
            emptyCursor.Setup(_ => _.MoveNextAsync(default)).ReturnsAsync(false);

            mockCollection.Setup(c => c.FindAsync(It.IsAny<FilterDefinition<Article>>(), null, default))
                .ReturnsAsync(emptyCursor.Object);

            var service = new ArticleService(mockCollection.Object);

            // Act
            var result = await service.GetAsync();

            // Assert
            Assert.Empty(result);
        }
    }
}