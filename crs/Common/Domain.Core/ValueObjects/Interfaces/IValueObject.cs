namespace Domain.Core.ValueObjects.Interfaces;

public interface IValueObject : IEquatable<IValueObject>
{
    /// <summary>
    /// Gets the equality components.
    /// </summary>
    /// <returns> The equality components.</returns>
    IEnumerable<object> GetEqualityComponents();
}
