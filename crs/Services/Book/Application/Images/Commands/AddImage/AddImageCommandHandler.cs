using Domain.Core.UnitOfWorks.Interfaces;
using Domain.SharedKernel.Enumerations;
using Domain.SharedKernel.Repositories;
using Infrastructure.FileStorage.Interfaces;

namespace Application.Images.Commands.AddImage;

internal sealed class AddImageCommandHandler(
    IUnitOfWork unitOfWork,
    IImageRepository repository,
    IImageService imageService)
    : ICommandHandler<AddImageCommand, Image>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IImageRepository _repository = repository;
    private readonly IImageService _imageService = imageService;

    public async Task<Result<Image>> Handle(AddImageCommand request, CancellationToken cancellationToken)
    {
        var imageType = ImageType.FromName(request.ImageType);

        var imageResult = await _imageService.UploadImageAsync(
            request.BucketName, request.ImageStream, imageType, cancellationToken);

        if (imageResult.IsFailure)
        {
            return imageResult;
        }

        var image = imageResult.Value;

        await _repository.AddAsync(image, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        return image;
    }
}
