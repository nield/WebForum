﻿using FluentValidation;
using WebForum.Application.Features.Posts.Commands.CreatePost;

namespace WebForum.Api.Models;

public class CreatePostRequest
{
    public string Title { get; set; }
    public string Content { get; set; }
}

public class CreatePostResponse
{
    public long Id { get; set; }
}

public class CreatePostRequestValidator: AbstractValidator<CreatePostRequest>
{
    public CreatePostRequestValidator()
    {
        RuleFor(x => x.Content).NotEmpty();
        RuleFor(x => x.Title).NotEmpty();
    }
}

public class CreatePostMapper : Profile
{
    public CreatePostMapper()
    {
        CreateMap<CreatePostRequest, CreatePostCommand>();
    }
}
