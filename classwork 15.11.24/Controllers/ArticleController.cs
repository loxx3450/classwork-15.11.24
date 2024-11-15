using AutoMapper;
using classwork.Core;
using classwork.Core.Interfaces;
using classwork.Core.Models;
using classwork_15._11._24.DTOs;
using classwork_15._11._24.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace classwork_15._11._24.Controllers
{
    [Authorize]
    [ApiController]
    [Route("articles")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly IMapper _mapper;

        public ArticleController(IArticleService articleService, IMapper mapper)
        {
            _articleService = articleService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleReturnDTO>>> GetArticles([FromQuery] string? region = null,
            [FromQuery] string? topic = null,
            [FromQuery] DateTime? from = null,
            [FromQuery] DateTime? until = null,
            [FromQuery] int skip = 0, 
            [FromQuery] int take = 10)
        {
            var filter = new ArticleFilter()
            {
                Region = region,
                Topic = topic,
                From = from,
                Until = until,
            };

            var articles = await _articleService.GetArticles(filter, skip, take);

            return Ok(_mapper.Map<IEnumerable<ArticleReturnDTO>>(articles));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleReturnDTO>> GetArticleById(int id)
        {
            var article = await _articleService.GetArticleById(id);

            if (article is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ArticleReturnDTO>(article));
        }

        [HttpPost]
        public async Task<ActionResult<ArticleReturnDTO>> AddArticle([FromBody] ArticleCreateDTO article)
        {
            var createdArticle = await _articleService.AddArticle(_mapper.Map<Article>(article));

            return Created($"articles/{createdArticle.Id}", _mapper.Map<ArticleReturnDTO>(createdArticle));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ArticleReturnDTO>> UpdateArticle([FromRoute] int id, [FromBody] ArticleCreateDTO article)
        {
            try
            {
                var updatedArticle = await _articleService.UpdateArticle(id, _mapper.Map<Article>(article));

                return Ok(updatedArticle);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArticle(int id)
        {
            await _articleService.DeleteArticle(id);

            return NoContent();
        }
    }
}
