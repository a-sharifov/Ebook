namespace Domain.SharedKernel.Errors;

public static class MoneyErrors
{
    public static Error CannotBeZeroOrNegative =>
        new("Money.CannotBeZeroOrNegative", "Money cannot be negative or zero");
}
