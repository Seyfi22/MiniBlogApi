using System.Text.Json.Serialization;

namespace MiniBlogApi.Model.Entities
{
    public class Category
    {
        public Category()
        {
            Articles = new HashSet<Article>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
