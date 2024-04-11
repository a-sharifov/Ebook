using Application.Common.DTOs.Images;
using Domain.Core.UnitOfWorks.Interfaces;
using Domain.SharedKernel.Enumerations;
using Domain.SharedKernel.Ids;
using Domain.SharedKernel.Repositories;
using Domain.SharedKernel.ValueObjects;
using Infrastructure.FileStorage.Interfaces;

namespace Application.Images.Commands.AddImage;

internal sealed class AddImageCommandHandler(
    IFileService fileService,
    IImageRepository repository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddImageCommand, ImageDto>
{
    private readonly IFileService _fileService = fileService;
    private readonly IImageRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ImageDto>> Handle(AddImageCommand request, CancellationToken cancellationToken)
    {
        var id = new ImageId(Guid.NewGuid());
        var bucketName = BucketName.Create(request.BucketName);
        var name = ImageName.Create(request.Name);
        var imageResult = Image.Create(id, bucketName.Value, name.Value);

        if (imageResult.IsFailure)
        {
            return Result.Failure<ImageDto>(
                imageResult.Error);
        }

        var image = imageResult.Value;

        await _fileService.UploadFileAsync(
            image.BucketName.Value, 
            image.Name.Value, 
            request.ImageStream, 
            cancellationToken);

        await _repository.AddAsync(image, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        var imageDto = image.Adapt<ImageDto>();
        return imageDto;
    }
}
