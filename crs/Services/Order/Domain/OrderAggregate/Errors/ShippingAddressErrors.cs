using Domain.Core.Errors;

namespace Domain.OrderAggregate.Errors;

public static class ShippingAddressErrors
{
    public static Error StreetCannotBeEmpty => new(
        "ShippingAddress.StreetCannotBeEmpty",
        "Shipping address street cannot be empty");
        
    public static Error CityCannotBeEmpty => new(
        "ShippingAddress.CityCannotBeEmpty",
        "Shipping address city cannot be empty");
        
    public static Error StateCannotBeEmpty => new(
        "ShippingAddress.StateCannotBeEmpty",
        "Shipping address state cannot be empty");
        
    public static Error CountryCannotBeEmpty => new(
        "ShippingAddress.CountryCannotBeEmpty",
        "Shipping address country cannot be empty");
        
    public static Error ZipCodeCannotBeEmpty => new(
        "ShippingAddress.ZipCodeCannotBeEmpty",
        "Shipping address zip code cannot be empty");
        
    public static Error InvalidZipCode => new(
        "ShippingAddress.InvalidZipCode",
        "Shipping address zip code is invalid");
}
