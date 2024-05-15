namespace Domain.UserAggregate.Errors;

public static class ResetPasswordTokenErrors
{
    public static Error CannotBeEmpty => 
        new("ResetPasswordToken", "Cannot be empty.");

}