using Domain.CartAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistence.Configurations.Converters;

internal sealed class CartExpirationTimeConverter : ValueConverter<CartExpirationTime, DateTime>
{
    public CartExpirationTimeConverter()
        : base(
            expirationTime => expirationTime.Value,
            value => CartExpirationTime.Create(value).Value)
    {
    }
}
