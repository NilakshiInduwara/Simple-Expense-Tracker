using ExpenseTracker.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data.Config
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(n => n.CategoryName).IsRequired().HasMaxLength(250);
            builder.Property(n => n.Description).HasMaxLength(250);

            builder.HasData(new List<Category>()
            {
                new Category
                {
                    Id = 1,
                    CategoryName = "Food",
                    Description = "Test Description"
                },
                new Category
                {
                    Id = 2,
                    CategoryName = "Transport",
                    Description = "Transport Description"
                }
            });
        }
    }
}
