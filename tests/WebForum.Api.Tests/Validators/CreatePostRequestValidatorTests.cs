using WebForum.Api.Models;

namespace WebForum.Api.Tests.Validators;

public class CreatePostRequestValidatorTests
{
    private readonly CreatePostRequestValidator _validator = new();

    [Fact]
    public void Given_EmptyContent_Should_HaveError()
    {
        var request = new CreatePostRequest { Content = "" };

        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Content);
    }

    [Fact]
    public void Given_EmptyTitle_Should_HaveError()
    {
        var request = new CreatePostRequest { Title = "" };

        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }
}
