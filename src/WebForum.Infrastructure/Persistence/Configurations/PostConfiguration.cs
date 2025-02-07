﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebForum.Infrastructure.Persistence.Configurations;

[ExcludeFromCodeCoverage]
public class PostConfiguration : BaseConfiguration<Post>
{
    public override string TableName => "Posts";

    public override void Configure(EntityTypeBuilder<Post> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Tags)
            .HasMaxLength(1000)
            .IsUnicode(false)
            .IsRequired(false); 

        builder.Property(x => x.Content).IsRequired();

        builder.HasMany(x => x.Comments)
            .WithOne(x => x.Post)
            .HasForeignKey(x => x.PostId);
    }
}