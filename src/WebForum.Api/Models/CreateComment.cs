using FluentValidation;

namespace WebForum.Api.Models;

public class CreateCommentRequest
{
    public required string Comment { get; set; }
}

public class CreateCommentResponse
{
    public long Id { get; set; }
}

public class CreateCommentRequestValidator : AbstractValidator<CreateCommentRequest>
{
    public CreateCommentRequestValidator()
    {
        RuleFor(x => x.Comment).NotEmpty();
    }
}