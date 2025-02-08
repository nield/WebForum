using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebForum.Infrastructure.Persistence.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Surname)
            .HasMaxLength(500)
            .IsRequired();
    }
}
