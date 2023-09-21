using KnowHows_Back_End.Models;

namespace KnowHows_Back_End.Interfaces
{
    public interface IArticleService
    {
        public Task<List<Article>> GetArticlesAsync();

        public Task CreateArticleAsync(Article newPost);
    }
}
