namespace Infrasctructure.Hashing.Interfaces;

public interface IHashingService
{
    string Hash(string input, string salt);
    bool Verify(string input, string salt, string hash);
    string GenerateSalt();
    string GenerateToken();
}