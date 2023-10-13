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
                .WithOrigins("*")
                .AllowAnyMethod()
                .AllowAnyHeader());

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
