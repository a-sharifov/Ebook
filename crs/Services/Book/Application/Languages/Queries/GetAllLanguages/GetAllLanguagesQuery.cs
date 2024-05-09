using Application.Common.DTOs.Languages;

namespace Application.Languages.Queries.GetAllLanguages;

public sealed record GetAllLanguagesQuery : IQuery<IEnumerable<LanguageDto>>;
