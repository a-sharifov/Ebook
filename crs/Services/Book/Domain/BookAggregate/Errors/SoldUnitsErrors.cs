namespace Domain.BookAggregate.Errors;

public static class SoldUnitsErrors
{
    public static Error SoldUnitsCannotBeNegative =>
        new("SoldUnits.SoldUnitsCannotBeNegative", "Sold units cannot be negative");
}