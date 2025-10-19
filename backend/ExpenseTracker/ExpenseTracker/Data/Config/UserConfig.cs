using ExpenseTracker.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Data.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(n => n.Name).IsRequired().HasMaxLength(250);
            builder.Property(n => n.Email).IsRequired().HasMaxLength(250);

            builder.HasData(new List<User>()
            {
                new User
                {
                    Id = 1,
                    Name = "Test Name",
                    Email = "Test Email",
                    Password = "Test PW",
                },
                new User
                {
                    Id = 2,
                    Name = "Test Name 2",
                    Email = "Test Email 2",
                    Password = "Test PW 2",
                }
            });
        }
    }
}
