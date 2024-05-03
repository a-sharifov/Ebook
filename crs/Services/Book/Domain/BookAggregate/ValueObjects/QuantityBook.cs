using Domain.BookAggregate.Errors;

namespace Domain.BookAggregate.ValueObjects;

public class QuantityBook
{
    public int Value { get; private set; }

    private QuantityBook(int value) => Value = value;

    public static Result<QuantityBook> Create(int quantity)
    {
        if (quantity < 0)
        {
            return Result.Failure<QuantityBook>(
                QuantityBookErrors.QuantityCannotBeNegative);
        }

        return new QuantityBook(quantity);
    }
}