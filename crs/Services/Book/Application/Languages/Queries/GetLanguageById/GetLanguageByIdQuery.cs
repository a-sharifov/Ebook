using Application.Common.DTOs.Languages;

namespace Application.Languages.Queries.GetLanguageById;

public sealed record GetLanguageByIdQuery(Guid Id) : IQuery<LanguageDto>;
