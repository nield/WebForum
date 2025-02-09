using System.Text;
using System.Text.Json;

namespace WebForum.Api.Integration.Tests.Controllers;

[Collection("WebApplicationCollection")]
public class AuthControllerTests
{
    private readonly WebApplicationFixture _webApplicationFixture;

    public AuthControllerTests(WebApplicationFixture webApplicationFixture)
    {
        _webApplicationFixture = webApplicationFixture;
    }

    [Fact]
    public async Task RegisterUser_ShouldReturn204()
    {
        try
        {
            var payload = new RegisterUserRequest
            {
                Email = "new@user.com",
                Password = "password",
                Name = "New",
                Surname = "User"
            };

            var sut = await _webApplicationFixture.HttpClient.PostAsync(
                "/api/auth/register",
                new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
            );

            sut.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
        finally
        {
            await _webApplicationFixture.ResetDatabaseAsync();
        }
    }
}
