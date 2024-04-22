using Domain.SharedKernel.Enumerations;
using Domain.SharedKernel.ValueObjects;

namespace Application.Images.Commands.AddImage;

internal sealed class AddImageCommandValidator : AbstractValidator<AddImageCommand>
{
    public AddImageCommandValidator()
    {
        RuleFor(x => x.BucketName)
            .MaximumLength(BucketName.MaxLength)
            .Must(ImageBucket.IsNameExists)
            .NotEmpty();

        RuleFor(x => x.Name)
            .MaximumLength(ImageName.MaxLength)
            .NotEmpty();

        RuleFor(x => x.ImageStream)
            .NotNull();
    }
} 
