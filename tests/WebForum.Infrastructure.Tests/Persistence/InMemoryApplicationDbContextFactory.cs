using WebForum.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using WebForum.Domain.Entities;
using WebForum.Application.Common.Constants;

namespace WebForum.Infrastructure.Tests.Persistence;

public static class InMemoryApplicationDbContextFactory
{
    public static ApplicationDbContext CreateContext(string databaseName)
    {
        var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>())
                .UseInMemoryDatabase(databaseName)
                .Options;

        var dbContext = new ApplicationDbContext(contextOptions);

        SeedData(dbContext);

        return dbContext;
    }

    private static void SeedData(ApplicationDbContext dbContext)
    {
        // Ensure existing seed data is saved before adding new data.
        dbContext.Database.EnsureCreated();

        var user = new User
        {
            Id = UserConstants.IntegrationTestUserId,
            Name = "test",
            Surname = "user",
            Email = "test@test.com",
            UserName = "test@test.com",
            Posts =
            [
                new Post
                {
                    Title = "Test Title",
                    Content = "Test Content",
                    CreatedDateTime = DateTimeOffset.Now,
                    Comments =
                    [
                        new Comment
                        {
                            Content = "Comment1",
                            CreatedDateTime = DateTimeOffset.Now
                        },
                        new Comment
                        {
                            Content = "Comment2",
                            CreatedDateTime = DateTimeOffset.Now
                        }
                    ],
                    Likes =
                    [
                        new Like
                        {
                            CreatedBy = UserConstants.IntegrationTestUserId
                        }
                    ]
                }
            ]
        };

        dbContext.Users.Add(user);

        dbContext.SaveChanges();
    }
}