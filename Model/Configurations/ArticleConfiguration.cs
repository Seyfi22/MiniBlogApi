using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniBlogApi.Model.Entities;

namespace MiniBlogApi.Model.Configurations
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> entity)
        {
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(255);

            entity.HasOne(a => a.Category)
                .WithMany(c => c.Articles)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
