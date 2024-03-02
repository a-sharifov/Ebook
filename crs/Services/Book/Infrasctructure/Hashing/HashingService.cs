using System.Security.Cryptography;
using System.Text;
using Infrasctructure.Hashing.Interfaces;

namespace Infrasctructure.Hashing;

public class HashingService : IHashingService
{
    public string GenerateSalt() => GenerateToken();

    public string Hash(string input, string salt)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(input + salt);
        byte[] hashBytes = SHA256.HashData(inputBytes);

        StringBuilder builder = new();
        for (int i = 0; i < hashBytes.Length; i++)
        {
            builder.Append(hashBytes[i].ToString("x2"));
        }
        return builder.ToString();
    }

    public bool Verify(string password, string salt, string hash)
    {
        var HashedPassword = Hash(password, salt);
        var isVerify = HashedPassword == hash;
        return isVerify;
    }

    private static byte[] ConvertToBytes(string value) =>
        Convert.FromBase64String(value);

    private static string ConvertToString(byte[] value) =>
        Convert.ToBase64String(value);

    public string GenerateToken()
    {
        byte[] saltBytes = RandomNumberGenerator.GetBytes(32);

        StringBuilder builder = new();
        for (int i = 0; i < saltBytes.Length; i++)
        {
            builder.Append(saltBytes[i].ToString("x2"));
        }
        return builder.ToString();
    }
}
