using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniBlogApi.Data;
using MiniBlogApi.Model.DTOs.Article;
using MiniBlogApi.Model.Entities;

namespace MiniBlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly BlogDbContext _context;

        public ArticleController(BlogDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllArticles()
        {
            var articles = _context.Articles
                .Include(a => a.Category)
                .Select(a => new ArticleDto
                {
                    Id = a.Id,
                    Title = a.Title,
                    Category = a.Category.Name
                }).ToList();

            return Ok(articles);
        }

        [HttpGet("{id}")]
        public IActionResult GetArticle(int id)
        {
            var article = _context.Articles
                .Include(a => a.Category)
                .Select(a => new ArticleDetailDto
                {
                    Id = a.Id,
                    Title = a.Title,
                    Content = a.Content,
                    Category = a.Category.Name,
                    CategoryId = a.Category.Id
                }).FirstOrDefault(a => a.Id == id); 

            if(article == null)
            {
                return NotFound($"No article with id {id}");
            }

            return Ok(article);
        } 

        [HttpPost]
        public IActionResult AddArticle([FromBody] CreateArticleDto articleDto)
        {
            if(articleDto == null)
            {
                return BadRequest("Article can not be null");
            }

            var category = _context.Categories.FirstOrDefault(c => c.Id == articleDto.CategoryId);

            if(category == null)
            {
                return BadRequest($"No category with id {articleDto.CategoryId}");
            }

            var article = new Article
            {
                Title = articleDto.Title,
                Content = articleDto.Content,
                CategoryId = category.Id
            };

            _context.Articles.Add(article);
            _context.SaveChanges();

            var response = new ArticleDto
            {
                Id = article.Id,
                Title = article.Title,
                Category = category.Name
            };

            return CreatedAtAction(nameof(GetArticle), new {id = article.Id}, response);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateArticle(int id, [FromBody] UpdateArticleDto articleDto)
        {
            var article = _context.Articles.Include(a => a.Category).FirstOrDefault(a => a.Id == id);


            if(article == null)
            {
                return NotFound($"No article with id {id}");
            }

            if (article.CategoryId != articleDto.CategoryId && !(_context.Categories.Any(c => c.Id == articleDto.CategoryId)))
            {
                return NotFound($"No category with id {articleDto.CategoryId}");
            }

            
            article.Title = articleDto.Title;
            article.Content = articleDto.Content;
            article.CategoryId = articleDto.CategoryId;

            _context.SaveChanges();


            var categoryName = _context.Categories
                .Where(c => c.Id == article.CategoryId)
                .Select(c => c.Name)
                .FirstOrDefault();


            var response = new ArticleDto
            {
                Id = article.Id,
                Title = article.Title,
                Category = categoryName
            };

            return Ok(response);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteArticle(int id)
        {
            var article = _context.Articles.FirstOrDefault(a => a.Id == id);

            if(article == null)
            {
                return NotFound($"No article with id {id}");
            }

            _context.Articles.Remove(article);
            _context.SaveChanges();

            return Ok($"Article with id {id} removed");
        }
    }
}
