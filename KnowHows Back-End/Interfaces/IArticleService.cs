using KnowHows_Back_End.Models;

namespace KnowHows_Back_End.Interfaces
{
    public interface IArticleService
    {
        public Task<List<Article>> GetArticlesAsync();

        public Task<Article?> GetArticleAsync(string id);

        public Task CreateArticleAsync(Article newPost);

        Task UpdateArticleAsync(string id, Article updateArticle);
    }
}
