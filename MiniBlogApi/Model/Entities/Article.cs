using System.Text.Json.Serialization;

namespace MiniBlogApi.Model.Entities
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
