using KnowHows_Back_End.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace KnowHows_Back_End.Services;

public class ArticlesService
{
    private readonly IMongoCollection<Article> _articlesCollection;

    public ArticlesService(
        IOptions<KnowHowsDatabaseSettings> knowHowsDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            knowHowsDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            knowHowsDatabaseSettings.Value.DatabaseName);

        _articlesCollection = mongoDatabase.GetCollection<Article>(
            knowHowsDatabaseSettings.Value.PostCollectionName);
    }

    public async Task<List<Article>> GetAsync() =>
        await _articlesCollection.Find(_ => true).ToListAsync();

    public async Task<Article?> GetAsync(string id) =>
        await _articlesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Article newPost) =>
        await _articlesCollection.InsertOneAsync(newPost);

    public async Task UpdateAsync(string id, Article updatedPost) =>
        await _articlesCollection.ReplaceOneAsync(x => x.Id == id, updatedPost);

    public async Task RemoveAsync(string id) =>
        await _articlesCollection.DeleteOneAsync(x => x.Id == id);
}