using KnowHows_Back_End.Interfaces;
using KnowHows_Back_End.Models;
using Microsoft.AspNetCore.Mvc;

namespace KnowHows_Back_End.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetTokenController : ControllerBase
    {
        private readonly IArticleService _iArticleService;

        public GetTokenController(IArticleService iArticlesService) =>
        _iArticleService = iArticlesService;


        //[HttpGet]
        //public async Task<ActionResult<List<Article>>> Get()
        //{
            
        //}
    }
}
