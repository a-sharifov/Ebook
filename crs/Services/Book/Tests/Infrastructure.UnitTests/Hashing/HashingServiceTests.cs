using Infrastructure.Hashing;

namespace Infrastructure.UnitTests.Hashing;

public class HashingServiceTests
{
    private readonly HashingService _hashingService;

    public HashingServiceTests()
    {
        _hashingService = new HashingService();
    }

    [Theory]
    [InlineData("Sa197605")]
    [InlineData("MySuperSecureP@ssw0rd")]
    public void HashingService_Hash_GeneratesCorrectHash(string password)
    {
        // Arrange
        var salt = _hashingService.GenerateSalt();

        // Act
        var hash = _hashingService.Hash(password, salt);

        // Assert
        Assert.NotEmpty(hash);
    }

    [Theory]
    [InlineData("2hwbxhjxeh1b")]
    [InlineData("deleSuperSecurePdekdkmeded")]
    public void HashingService_Verify_Password(string password)
    {
        // Arrange
        var salt = _hashingService.GenerateSalt();
        var hash = _hashingService.Hash(password, salt);

        // Act
        var isVerified = _hashingService.Verify(password, salt, hash);

        // Assert
        Assert.True(isVerified);
    }

    [Fact]
    public void HashingService_GenerateSalt_Returns_NonEmptyString()
    {
        // Act
        var salt = _hashingService.GenerateSalt();

        // Assert
        Assert.NotEmpty(salt);
    }

    [Fact]
    public void HashingService_GenerateToken_Returns_NonEmptyString()
    {
        // Act
        var token = _hashingService.GenerateToken();

        // Assert
        Assert.NotEmpty(token);
    }
}
