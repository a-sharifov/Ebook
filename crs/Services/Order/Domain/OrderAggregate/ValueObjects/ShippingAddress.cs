using Domain.Core.ValueObjects;
using Domain.OrderAggregate.Errors;

namespace Domain.OrderAggregate.ValueObjects;

public sealed class ShippingAddress : ValueObject
{
    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string Country { get; }
    public string ZipCode { get; }

    private ShippingAddress(string street, string city, string state, string country, string zipCode)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
    }

    public static Result<ShippingAddress> Create(string street, string city, string state, string country, string zipCode)
    {
        if (string.IsNullOrWhiteSpace(street))
        {
            return Result.Failure<ShippingAddress>(ShippingAddressErrors.StreetCannotBeEmpty);
        }

        if (string.IsNullOrWhiteSpace(city))
        {
            return Result.Failure<ShippingAddress>(ShippingAddressErrors.CityCannotBeEmpty);
        }

        if (string.IsNullOrWhiteSpace(state))
        {
            return Result.Failure<ShippingAddress>(ShippingAddressErrors.StateCannotBeEmpty);
        }

        if (string.IsNullOrWhiteSpace(country))
        {
            return Result.Failure<ShippingAddress>(ShippingAddressErrors.CountryCannotBeEmpty);
        }

        if (string.IsNullOrWhiteSpace(zipCode))
        {
            return Result.Failure<ShippingAddress>(ShippingAddressErrors.ZipCodeCannotBeEmpty);
        }

        return Result.Success(new ShippingAddress(street, city, state, country, zipCode));
    }

    public override string ToString()
    {
        return $"{Street}, {City}, {State}, {Country}, {ZipCode}";
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return State;
        yield return Country;
        yield return ZipCode;
    }
}
