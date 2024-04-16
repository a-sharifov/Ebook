using Application.Common.DTOs.Languages;

namespace Application.Languages.Queries.GetLanguages;

public sealed record GetLanguagesQuery() : IQuery<IEnumerable<LanguageDto>>;
