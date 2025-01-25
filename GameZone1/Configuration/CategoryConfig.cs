using GameZone.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameZone.Configuration
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
                builder.HasData(new Category[]
                {
                    new Category { Id = 1, Name="Sports" },
                    new Category { Id = 2, Name="Action" },
                    new Category { Id = 3, Name = "Adventure" },
                    new Category { Id = 4, Name = "Racing" },
                    new Category { Id = 5, Name = "Fighting" },
                    new Category { Id = 6, Name = "Film" }
                });

        }
    }
}
