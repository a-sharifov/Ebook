using Domain.SharedKernel.Entities;
using Domain.SharedKernel.Ids;

namespace Domain.SharedKernel.Repositories;

public interface IImageRepository : IBaseRepository<Image, ImageId>
{
}
