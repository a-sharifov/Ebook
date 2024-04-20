using Application.Core.MappingConfig.Interfaces;
using Domain.BookAggregate;

namespace Application.Common.DTOs.Books;

public sealed class BookMappingConfig : IMappingConfig
{
    public void Configure() =>
        TypeAdapterConfig<Book, BookDto>
        .NewConfig()
        .Map(dest => dest.Id, src => src.Id.Value)
        .Map(dest => dest.Title, src => src.Title.Value)
        .Map(dest => dest.ISBN, src => src.ISBN.Value)
        .Map(dest => dest.PageCount, src => src.PageCount)
        .Map(dest => dest.Price, src => src.Price.Value)
        .Map(dest => dest.Poster, src => src.Poster);
}
