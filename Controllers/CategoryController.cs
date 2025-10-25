using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniBlogApi.Data;
using MiniBlogApi.Model.DTOs.Category;
using MiniBlogApi.Model.Entities;

namespace MiniBlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly BlogDbContext _context;

        public CategoryController(BlogDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = _context.Categories
                .Include(c => c.Articles)
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Articles = c.Articles.Select(a => new ArticleInCategoryDto
                    {
                        Id = a.Id,
                        Title = a.Title
                    }).ToList()
                }).ToList();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = _context.Categories
                .Include(c => c.Articles)
                .Select(c => new CategoryDetailDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Articles = c.Articles.Select(a => new ArticleDetailInCategoryDto
                    {
                        Id = a.Id,
                        Title = a.Title,
                        Content = a.Content
                    }).ToList()
                }).FirstOrDefault(c => c.Id == id); 

            if(category == null)
            {
                return NotFound($"No category with id {id}");
            }

            return Ok(category);
        }

        [HttpPost]
        public IActionResult AddCategory([FromBody] CreateCategoryDto categoryDto)
        {
            if(categoryDto == null)
            {
                return BadRequest("Category can not be null");
            }

            var category = new Category
            {
                Name = categoryDto.Name,
                Articles = categoryDto.Articles?.Select(a => new Article
                {
                    Title = a.Title,
                    Content = a.Content
                }).ToList()
            };

            _context.Categories.Add(category);
            _context.SaveChanges();

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Articles = category.Articles?.Select(a => new ArticleInCategoryDto
                {
                    Id = a.Id,
                    Title = a.Title
                }).ToList()
            };

            return CreatedAtAction(nameof(GetCategory), new {id = category.Id}, response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] UpdateCategoryDto categoryDto)
        {
            var category = _context.Categories.Include(c => c.Articles).FirstOrDefault(c => c.Id == id);

            if(category == null)
            {
                return NotFound($"No category with id {id}");
            }

            category.Name = categoryDto.Name;
            _context.SaveChanges();


            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Articles = category.Articles.Select(a => new ArticleInCategoryDto
                {
                    Id = a.Id,
                    Title = a.Title
                }).ToList()
            };


            return Ok(response);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if(category == null)
            {
                return NotFound($"No category with id {id}");
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return Ok($"Category with id {id} removed (and related articles removed)");
        }
    }   
}
