using MongoDB.Bson;
using MongoDB.Driver;
using KnowHows_Back_End.Services;
using KnowHows_Back_End.Models;
using KnowHows_Back_End.Interfaces;
using KnowHows_Back_End.Authentication;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<KnowHowsDatabaseSettings>(
    builder.Configuration.GetSection("KnowHowsDB"));

builder.Services.AddSingleton<ArticleService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme()
    {
        Description = "Api Key to secure the API",
        Type = SecuritySchemeType.ApiKey,
        Name = AuthConfig.ApiKeyHeader,
        In = ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });

    var scheme = new OpenApiSecurityScheme()
    {
        Reference = new OpenApiReference()
        {
            Type = ReferenceType.SecurityScheme,
            Id = "ApiKey"
        },
        In = ParameterLocation.Header
    };

    var requirement = new OpenApiSecurityRequirement()
    {
        { scheme, new List<string>() }
    };

    x.AddSecurityRequirement(requirement);
});

builder.Services.AddScoped<IArticleService, ArticleService>();

builder.Services.AddCors();

builder.Services.AddScoped<ApiKeyAuthenticationFilter>();

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

app.UseHttpsRedirection();

app.UseMiddleware<ApiKeyMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
