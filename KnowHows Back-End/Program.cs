using MongoDB.Bson;
using MongoDB.Driver;
using KnowHows_Back_End.Services;
using KnowHows_Back_End.Models;
using KnowHows_Back_End.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<KnowHowsDatabaseSettings>(
    builder.Configuration.GetSection("KnowHowsDB"));

builder.Services.AddSingleton<ArticleService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IArticleService, ArticleService>();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
                .WithOrigins("http://localhost:3000", "http://localhost:3001", "http://knowhows.com")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
