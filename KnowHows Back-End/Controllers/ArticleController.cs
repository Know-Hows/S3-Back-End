using KnowHows_Back_End.Interfaces;
using KnowHows_Back_End.Models;
using KnowHows_Back_End.Services;
using Microsoft.AspNetCore.Mvc;

namespace KnowHows_Back_End.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticleController : ControllerBase
{
    private readonly IArticleService _iArticleService;

    public ArticleController(IArticleService iArticlesService) =>
        _iArticleService = iArticlesService;

    [HttpGet]
    public async Task<ActionResult<List<Article>>> Get()
    {
        var articles = await _iArticleService.GetArticlesAsync();

        return articles != null ? Ok(articles) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult> Post(Article newArticle)
    {
        if (!ModelState.IsValid || newArticle.Title == null || newArticle.Body == null)
        {
            return BadRequest();
        }
        await _iArticleService.CreateArticleAsync(newArticle);

        return CreatedAtAction(nameof(Get), new { id = newArticle.Id }, newArticle);
    }

    [HttpPut("updateLikes/{id:length(24)}")]
    public async Task<ActionResult> UpdateLikesScore(string id, bool likeCredit)
    {
        if (id is null) return BadRequest();

        var article = await _iArticleService.GetArticleAsync(id);

        if (article == null) return NotFound();

        if (likeCredit == true) article.LikesScore++;
        else article.LikesScore--;

        await _iArticleService.UpdateArticleAsync(id, article);
        return Ok();
    }
}