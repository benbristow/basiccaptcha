using BasicCaptcha.Providers;
using FakeItEasy;

namespace BasicCaptcha.UnitTests;

public sealed class CaptchaServiceTests
{
    private const string Token = "a-token";

    private readonly BaseProvider _provider = A.Fake<BaseProvider>();
    private readonly HttpClient _httpClient = A.Fake<HttpClient>();
    private readonly CaptchaService _underTest;

    public CaptchaServiceTests()
    {
        _underTest = new CaptchaService(_provider, _httpClient);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task VerifyTokenAsync_WhenProviderReturnsBoolean_ReturnsExpectedResult(bool expected)
    {
        // Arrange
        A.CallTo(() => _provider.VerifyTokenAsync(Token, _httpClient)).Returns(expected);

        // Act
        var result = await _underTest.VerifyTokenAsync(Token);

        // Assert
        result.Should().Be(expected);
        A.CallTo(() => _provider.VerifyTokenAsync(Token, _httpClient)).MustHaveHappenedOnceExactly();
    }
}
