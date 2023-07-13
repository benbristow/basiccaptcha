using BasicCaptcha.Providers;

namespace BasicCaptcha.UnitTests.Providers;

public sealed class DummyProviderTests
{
    private readonly DummyProvider _underTest = new();

    [Fact]
    public async Task VerifyTokenAsync_WhenTokenIsFail_ReturnsFalse()
    {
        // Act
        var result = await _underTest.VerifyTokenAsync("fail");

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData("not-fail")]
    [InlineData(null)]
    public async Task VerifyTokenAsync_WhenTokenIsNotFail_ReturnsTrue(string? token)
    {
        // Act
        var result = await _underTest.VerifyTokenAsync(token);

        // Assert
        result.Should().BeTrue();
    }
}
