namespace MiniBlogApi.Model.DTOs.Article
{
    public class ArticleDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
    }
}
