using KnowHows_Back_End.Models;
using KnowHows_Back_End.Services;
using Microsoft.AspNetCore.Mvc;

namespace KnowHows_Back_End.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticleController : ControllerBase
{
    private readonly ArticlesService _articleService;

    public ArticleController(ArticlesService articlesService) =>
        _articleService = articlesService;

    [HttpGet]
    public async Task<List<Article>> Get() =>
        await _articleService.GetAsync();

    [HttpPost]
    public async Task<IActionResult> Post(Article newArticle)
    {
        await _articleService.CreateAsync(newArticle);

        return CreatedAtAction(nameof(Get), new { id = newArticle.Id }, newArticle);
    }
}