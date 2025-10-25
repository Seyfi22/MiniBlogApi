namespace MiniBlogApi.Model.DTOs.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ArticleInCategoryDto> Articles { get; set; }
    }
}
