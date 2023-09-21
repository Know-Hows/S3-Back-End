﻿using KnowHows_Back_End.Interfaces;
using KnowHows_Back_End.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace KnowHows_Back_End.Services;

public class ArticleService : IArticleService
{
    private readonly IMongoCollection<Article> _articlesCollection;

    public ArticleService(
        IOptions<KnowHowsDatabaseSettings> knowHowsDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            knowHowsDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            knowHowsDatabaseSettings.Value.DatabaseName);

        _articlesCollection = mongoDatabase.GetCollection<Article>(
            knowHowsDatabaseSettings.Value.ArticleCollectionName);
    }

    public async Task<List<Article>> GetArticlesAsync() =>
        await _articlesCollection.Find(_ => true).ToListAsync();

    public async Task CreateArticleAsync(Article newPost) =>
        await _articlesCollection.InsertOneAsync(newPost);
}