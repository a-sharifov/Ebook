namespace Domain.WishAggregate.Errors;

public static class WishErrors
{
    public static Error ThisBookIsExist =>
        new("WishThisBookIsExist", "This book is exist in wish list");

}
