namespace Domain.BookAggregate.Errors;

public static class QuantityBookErrors
{
    public static Error QuantityCannotBeNegative =>
        new("QuantityBook.QuantityCannotBeNegative", "Quantity cannot be negative");
}