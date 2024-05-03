namespace Domain.CartAggregate.Errors;

public static class CartExpirationTimeErrors
{
    public static Error CannotBeExpired =>
      new("CartExpirationTime.CannotBeExpired", "Cart expiration time should not be expired");
}
