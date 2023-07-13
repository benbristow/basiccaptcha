using System.Net;
using BasicCaptcha.Exceptions;
using BasicCaptcha.Providers;
using JustEat.HttpClientInterception;

namespace BasicCaptcha.UnitTests.Providers;

public sealed class GoogleRecaptchaProviderTests
{
    private const string SecretKey = "a-secret-key";
    private const string Token = "a-token";

    private readonly GoogleRecaptchaProvider _underTest;

    public GoogleRecaptchaProviderTests()
    {
        _underTest = new GoogleRecaptchaProvider(SecretKey);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task VerifyTokenAsync_WhenGoogleReturns_ReturnsSuccessResult(bool expected)
    {
        // Arrange
        var builder = new HttpRequestInterceptionBuilder()
            .Requests()
            .ForPost()
            .ForHttps()
            .ForHost("www.google.com")
            .ForPath("/recaptcha/api/siteverify")
            .ForFormContent(new List<KeyValuePair<string, string>> { new("secret", SecretKey), new("response", Token) })
            .Responds()
            .WithJsonContent(new GoogleRecaptchaProvider.GoogleSiteVerifyResponse()
            {
                Success = expected
            });
        var options = new HttpClientInterceptorOptions { ThrowOnMissingRegistration = true }.Register(builder);
        using var httpClient = options.CreateHttpClient();

        // Act
        var result = await _underTest.VerifyToken(Token, httpClient);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public async Task VerifyTokenAsync_WhenTokenIsNullOrWhitespace_ReturnsFalse()
    {
        // Arrange
        var builder = new HttpRequestInterceptionBuilder();
        var options = new HttpClientInterceptorOptions { ThrowOnMissingRegistration = true }.Register(builder);
        using var httpClient = options.CreateHttpClient();

        // Act
        var result = await _underTest.VerifyToken(null, httpClient);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task VerifyTokenAsync_WhenHttpRequestFails_ThrowsException()
    {
        // Arrange
        var builder = new HttpRequestInterceptionBuilder()
            .Requests()
            .ForPost()
            .ForHttps()
            .ForHost("www.google.com")
            .ForPath("/recaptcha/api/siteverify")
            .ForFormContent(new List<KeyValuePair<string, string>> { new("secret", SecretKey), new("response", Token) })
            .Responds()
            .WithStatus(HttpStatusCode.InternalServerError);
        var options = new HttpClientInterceptorOptions { ThrowOnMissingRegistration = true }.Register(builder);
        using var httpClient = options.CreateHttpClient();

        // Act
        var exception = await Record.ExceptionAsync(() => _underTest.VerifyToken(Token, httpClient));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<CaptchaException>();
        exception!.Message.Should().Be("Unable to contact Google RECAPTCHA service");
    }
}

