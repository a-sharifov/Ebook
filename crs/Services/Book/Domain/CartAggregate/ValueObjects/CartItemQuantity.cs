using Domain.CartAggregate.Errors;

namespace Domain.CartAggregate.ValueObjects;

public class CartItemQuantity : ValueObject
{
    public int Value { get; private set; }

    private CartItemQuantity(int value) => Value = value;

    public static Result<CartItemQuantity> Create(int quantity)
    {
        if (quantity <= 0)
        {
            return Result.Failure<CartItemQuantity>(
                CartItemQuantityErrors.QuantityCannotBeNegativeOrNull);
        }

        return new CartItemQuantity(quantity);
    }

    public static CartItemQuantity Default() => new(1);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator int(CartItemQuantity quantity) =>
        quantity.Value;
}
