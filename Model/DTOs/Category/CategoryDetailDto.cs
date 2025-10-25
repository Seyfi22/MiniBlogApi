namespace MiniBlogApi.Model.DTOs.Category
{
    public class CategoryDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ArticleDetailInCategoryDto> Articles { get; set; }
    }
}
