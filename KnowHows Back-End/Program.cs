using MongoDB.Bson;
using MongoDB.Driver;
using KnowHows_Back_End.Services;
using KnowHows_Back_End.Models;
using KnowHows_Back_End.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<KnowHowsDatabaseSettings>(
    builder.Configuration.GetSection("KnowHowsDB"));

builder.Services.AddSingleton<ArticleService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IArticleService, ArticleService>();

builder.Services.AddCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
                .WithOrigins("*", "http://localhost:30000", "http://front-end-clusterip-srv:30000", "http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader());

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
