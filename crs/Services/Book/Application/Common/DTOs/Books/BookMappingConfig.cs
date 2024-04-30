using Application.Core.MappingConfig.Interfaces;
using Contracts.Paginations;
using Domain.BookAggregate;

namespace Application.Common.DTOs.Books;

public sealed class BookMappingConfig : IMappingConfig
{
    public void Configure()
    {
        TypeAdapterConfig<Book, BookDto>
        .NewConfig()
        .Map(dest => dest.Id, src => src.Id.Value)
        .Map(dest => dest.Title, src => src.Title.Value)
        .Map(dest => dest.PageCount, src => src.PageCount.Value)
        .Map(dest => dest.Price, src => src.Price.Value)
        .Map(dest => dest.SoldUnits, src => src.SoldUnits.Value)
        .Map(dest => dest.Quantity, src => src.Quantity.Value)
        .Map(dest => dest.PosterUrl, src => src.Poster.Url.Value);

        TypeAdapterConfig<PagedList<Book>, PagedList<BookDto>>.NewConfig()
    .Map(dest => dest.Items, src => src.Items.Adapt<IEnumerable<BookDto>>())
    .Map(dest => dest.CurrentPage, src => src.CurrentPage)
    .Map(dest => dest.TotalPages, src => src.TotalPages)
    .Map(dest => dest.PageSize, src => src.PageSize)
    .Map(dest => dest.TotalCount, src => src.TotalCount);

    }
}
