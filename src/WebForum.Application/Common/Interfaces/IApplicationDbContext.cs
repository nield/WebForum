using Microsoft.EntityFrameworkCore;

namespace WebForum.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Post> Posts { get; }
    DbSet<Comment> Comments { get; }
}
