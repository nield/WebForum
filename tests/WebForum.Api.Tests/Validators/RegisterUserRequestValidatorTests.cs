using WebForum.Api.Models;

namespace WebForum.Api.Tests.Validators;

public class RegisterUserRequestValidatorTests
{
    private readonly RegisterUserRequestValidator _validator = new();

    [Fact]
    public void Given_EmptyEmail_Should_HaveError()
    {
        var request = new RegisterUserRequest { Email = "" };

        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Theory]
    [InlineData("test")]
    [InlineData("test@")]
    [InlineData("@test")]
    public void Given_InvalidEmail_Should_HaveError(string email)
    {
        var request = new RegisterUserRequest { Email = email };

        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void Given_EmptyPassword_Should_HaveError()
    {
        var request = new RegisterUserRequest { Password = "" };

        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void Given_EmptName_Should_HaveError()
    {
        var request = new RegisterUserRequest { Name = "" };

        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Given_EmptSurname_Should_HaveError()
    {
        var request = new RegisterUserRequest { Surname = "" };

        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Surname);
    }
}
