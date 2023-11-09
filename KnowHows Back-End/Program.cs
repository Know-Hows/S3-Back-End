using MongoDB.Bson;
using MongoDB.Driver;
using KnowHows_Back_End.Services;
using KnowHows_Back_End.Models;
using KnowHows_Back_End.Interfaces;
using KnowHows_Back_End.Authentication;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using KnowHows_Back_End.Authentication.Auth0;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<KnowHowsDatabaseSettings>(
    builder.Configuration.GetSection("KnowHowsDB"));

builder.Services.AddSingleton<ArticleService>();

builder.Services.AddControllers();

// 1. Add Authentication Services
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//    options.Authority = "https://dev-n6hluorismz6kc2k.eu.auth0.com/";
//    options.Audience = "http://localhost:5201";
//});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}/";
    options.Audience = builder.Configuration["Auth0:Audience"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = ClaimTypes.NameIdentifier
    };
    Console.WriteLine(options.TokenValidationParameters.NameClaimType);
});

builder.Services
      .AddAuthorization(options =>
      {
          options.AddPolicy(
            "read:messages",
            policy => policy.Requirements.Add(
              new HasScopeRequirement("read:messages", builder.Configuration["Auth0:Domain"]/*domain*/)
            )
          );
      });
builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

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

//builder.Services.AddScoped<ApiKeyAuthenticationFilter>();

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

//app.UseMiddleware<ApiKeyMiddleware>();

app.UseAuthorization();

// 2. Enable authentication middleware
app.UseAuthentication();

app.MapControllers();

app.Run();
