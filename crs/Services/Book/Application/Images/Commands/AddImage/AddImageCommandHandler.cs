using Application.Common.DTOs.Images;
using Domain.Core.UnitOfWorks.Interfaces;
using Domain.SharedKernel.Enumerations;
using Domain.SharedKernel.Ids;
using Domain.SharedKernel.Repositories;
using Domain.SharedKernel.ValueObjects;
using Infrastructure.FileStorages.Interfaces;

namespace Application.Images.Commands.AddImage;

internal sealed class AddImageCommandHandler(
    IFileService fileService,
    IImageRepository repository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddImageCommand, ImageDto>
{
    //private readonly BaseUrlOptions urlOptions = ;
    private readonly IFileService _fileService = fileService;
    private readonly IImageRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ImageDto>> Handle(AddImageCommand request, CancellationToken cancellationToken)
    {
        var id = new ImageId(Guid.NewGuid());
        var name = ImageName.Create(request.Name).Value;
        var bucketName = ImageBucket.FromName(request.BucketName);
        var permanentUrl = _fileService.GeneratePermanentUrl(bucketName.Name, name.Value);
        var url = ImageUrl.Create(permanentUrl).Value;
        var image = Image.Create(id, bucketName, name, url).Value;

        await _fileService.UploadFileAsync(
            image.Bucket.Name, 
            image.Name.Value, 
            request.ImageStream, 
            cancellationToken);

        await _repository.AddAsync(image, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        var imageDto = image.Adapt<ImageDto>();
        return imageDto;
    }
}
