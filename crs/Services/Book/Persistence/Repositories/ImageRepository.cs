using Catalog.Persistence.Caching.Abstractions;
using Persistence.DbContexts;
using Domain.SharedKernel.Entities;
using Domain.SharedKernel.Ids;
using Domain.SharedKernel.Repositories;

namespace Persistence.Repositories;

public class ImageRepository(
    BookDbContext dbContext,
    ICachedEntityService<Image, ImageId> cached)
    : BaseRepository<Image, ImageId>(
        dbContext,
        cached,
        expirationTime: TimeSpan.FromMinutes(20)), IImageRepository
{
}