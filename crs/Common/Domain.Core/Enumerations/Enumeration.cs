using Domain.Core.Enumerations.Interfaces;

namespace Domain.Core.Enumerations;

/// <summary>
/// The enumeration base class.
/// </summary>
/// <typeparam name="TEnum">The enumeration type.</typeparam>
/// <param name="index">The index.</param>
/// <param name="name">The name.</param>
public abstract class Enumeration<TEnum>(int index, string name) 
    : IEnumeration
    where TEnum : Enumeration<TEnum>
{
    /// <summary>
    /// Get the index - is key.
    /// </summary>
    public int Index { get; private set; } = index;

    /// <summary>
    /// Gets the name - is value.
    /// </summary>
    public string Name { get; private set; } = name;

    /// <summary>
    /// Gets the enumeration.
    /// </summary>
    private static readonly Dictionary<int, TEnum> _enumerations = GetEnumerations();

    /// <summary>
    /// Return the Enum from value or default.
    /// </summary>
    /// <param name="index"> The value.</param>
    public static TEnum? FromValueOrDefault(int index) =>
        _enumerations.TryGetValue(index, out var enumeration)
        ? enumeration : null;

    /// <summary>
    /// Return the enum from name.
    /// </summary>
    /// <param name="name">The name.</param>
    public static TEnum FromName(string name) =>
        _enumerations.Values.Single(x => x.Name == name);

    /// <summary>
    /// Return is name exists or not.
    /// </summary>
    /// <param name="name">The name.</param>
    public static bool IsNameExists(string name) =>
        _enumerations.Values.Select(x => x.Name).Contains(name);

    /// <summary>
    /// Return the enum from name or default.
    /// </summary>
    /// <param name="name"></param>
    public static TEnum? FromNameOrDefault(string name) =>
      _enumerations.Values.SingleOrDefault(x => x.Name == name);

    /// <summary>
    /// Return all names.
    /// </summary>
    public static IEnumerable<string> GetNames() =>
        _enumerations.Values.Select(x => x.Name);

    /// <summary>
    /// Name exists.
    /// </summary>
    public static bool NameExists(string name) =>
        _enumerations.Values.Any(x => x.Name == name);

    /// <summary>
    /// Value exists.
    /// </summary>
    public static bool ValueExists(int index) =>
        _enumerations.ContainsKey(index);

    private static Dictionary<int, TEnum> GetEnumerations()
    {
        var enumerationType = typeof(TEnum);

        var fields = enumerationType.GetFields(
            BindingFlags.Public |
            BindingFlags.Static |
            BindingFlags.DeclaredOnly)
            .Where(fieldInfo => enumerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo => (TEnum)fieldInfo.GetValue(default)!);

        return fields.ToDictionary(field => field.Index);
    }

    public static implicit operator int(Enumeration<TEnum> @enum) =>
        @enum.Index;

    public static implicit operator string(Enumeration<TEnum> @enum) =>
        @enum.Name;
}