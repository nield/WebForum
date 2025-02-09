using WebForum.Api.Models;

namespace WebForum.Api.Tests.Validators;

public class CreateCommentRequestValidatorTests
{
    private readonly CreateCommentRequestValidator _validator = new();

    [Fact]
    public void Given_EmptyComment_Should_HaveError()
    {
        var request = new CreateCommentRequest { Comment = "" };

        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Comment);
    }
}
