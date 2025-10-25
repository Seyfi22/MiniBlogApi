namespace MiniBlogApi.Model.DTOs.Category
{
    public class CreateCategoryDto
    {
        public string Name { get; set; }

        public ICollection<CreateArticleInCreateCategoryDto>? Articles { get; set; }
    }
}
