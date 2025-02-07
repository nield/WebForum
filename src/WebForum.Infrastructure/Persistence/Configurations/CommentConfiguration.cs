using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebForum.Infrastructure.Persistence.Configurations;

public class CommentConfiguration : BaseConfiguration<Comment>
{
    public override string TableName => "Comments";

    public override void Configure(EntityTypeBuilder<Comment> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Content).IsRequired();

        builder.Property(x => x.PostId).IsRequired();

        builder.HasIndex(x => x.PostId);
    }
}
