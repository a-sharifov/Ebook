namespace Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required");

        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Street is required");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required");

        RuleFor(x => x.State)
            .NotEmpty().WithMessage("State is required");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required");

        RuleFor(x => x.ZipCode)
            .NotEmpty().WithMessage("Zip code is required");

        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("Order must contain at least one item");

        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(i => i.BookId)
                .NotEmpty().WithMessage("Book ID is required");

            item.RuleFor(i => i.BookTitle)
                .NotEmpty().WithMessage("Book title is required");

            item.RuleFor(i => i.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Unit price must be greater than or equal to 0");

            item.RuleFor(i => i.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0");
        });
    }
}
