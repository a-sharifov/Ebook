namespace Contracts.Extensions;

/// <summary>
/// Class for string extension.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Checks if the string is null or empty.
    /// </summary>
    /// <param name="str"> The string to check.</param>
    /// <returns> True if the string is null or empty.</returns>
    public static bool IsNullOrEmpty(this string? str) =>
        string.IsNullOrEmpty(str);

    /// <summary>
    /// Checks if the string is not null or empty.
    /// </summary>
    /// <param name="str"> The string to check.</param>
    /// <returns> True if the string is not null or empty.</returns>
    public static bool IsNotNullOrEmpty(this string str) =>
        !str.IsNullOrEmpty();

    /// <summary>
    /// Checks if the string is null or white space.
    /// </summary>
    /// <param name="str"> The string to check.</param>
    /// <returns> True if the string is null or white space.</returns>
    public static bool IsNullOrWhiteSpace(this string? str) =>
        string.IsNullOrWhiteSpace(str);

    /// <summary>
    /// Checks if the string is not null or white space.
    /// </summary>
    /// <param name="str"> The string to check.</param>
    /// <returns> True if the string is not null or white space.</returns>
    public static bool IsNotNullOrWhiteSpace(this string? str) =>
        !str.IsNullOrWhiteSpace();

    /// <summary>
    /// checks if the string is lowest or not.
    /// </summary>
    /// <param name="str"> The string to check.</param>
    /// <returns>True if the all char lowest.</returns>
    public static bool IsLowerCase(this string str) =>
        str.All(char.IsLower);

    public static bool IsNotNullOrWhiteSpace(params string[] strs)
    {
        foreach (var str in strs)
        {
            var isNotNullOrWhiteSpace = str.IsNotNullOrWhiteSpace();
            if (!isNotNullOrWhiteSpace)
            {
                return false;
            }
        }

        return true;
    }

    public static bool IsNullOrWhiteSpace(params string[] strs)
    {
        foreach (var str in strs)
        {
            var isNotNullOrWhiteSpace = str.IsNullOrWhiteSpace();
            if (!isNotNullOrWhiteSpace)
            {
                return false;
            }
        }

        return true;
    }
}
