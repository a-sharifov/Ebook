using Domain.SharedKernel.Enumerations;
using Domain.SharedKernel.ValueObjects;

namespace Application.Images.Commands.AddImage;

internal sealed class AddImageCommandValidator : AbstractValidator<AddImageCommand>
{
    public AddImageCommandValidator()
    {
        RuleFor(x => x.BucketName)
            .MaximumLength(BucketName.MaxLength)
            .NotEmpty();

        RuleFor(x => x.Name)
            .Must(ImageBucket.IsNameExists)
            .MaximumLength(ImageName.MaxLength)
            .NotEmpty();

        RuleFor(x => x.ImageStream)
            .NotNull();
    }
} 
