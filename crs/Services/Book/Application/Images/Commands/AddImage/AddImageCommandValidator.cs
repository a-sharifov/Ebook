using Domain.SharedKernel.Enumerations;

namespace Application.Images.Commands.AddImage;

internal sealed class AddImageCommandValidator : AbstractValidator<AddImageCommand>
{
    public AddImageCommandValidator()
    {
        RuleFor(x => x.BucketName)
            .NotEmpty();

        RuleFor(x => x.ImageStream)
            .NotNull();

        RuleFor(x => x.ImageType)
            .Must(x => ImageType.FromNameOrDefault(x) is not null);
    }
}
