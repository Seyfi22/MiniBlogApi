namespace MiniBlogApi.Model.DTOs.Article
{
    public class CreateArticleDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
    }
}
