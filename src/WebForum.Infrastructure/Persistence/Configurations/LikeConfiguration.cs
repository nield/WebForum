using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebForum.Infrastructure.Persistence.Configurations;

public class LikeConfiguration : BaseConfiguration<Like>
{
    public override string TableName => "PostLikes";

    public override void Configure(EntityTypeBuilder<Like> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.PostId).IsRequired();

        builder.HasIndex(x => x.PostId);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Likes)
            .HasForeignKey(x => x.CreatedBy);
    }
}
