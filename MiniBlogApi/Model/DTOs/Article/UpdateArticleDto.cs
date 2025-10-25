namespace MiniBlogApi.Model.DTOs.Article
{
    public class UpdateArticleDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
    }
}
