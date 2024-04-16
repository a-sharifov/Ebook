using Application.Core.MappingConfig.Interfaces;
using Domain.UserAggregate;

namespace Application.Common.DTOs.Users;

public sealed class UserMappingConfig : IMappingConfig
{
    public void Configure() =>
       TypeAdapterConfig<User, UserDto>
       .NewConfig()
       .Map(dest => dest.Id, src => src.Id.Value)
       .Map(dest => dest.Role, src => src.Role.Name)
       .Map(dest => dest.Email, src => src.Email.Value)
       .Map(dest => dest.FirstName, src => src.FirstName.Value)
       .Map(dest => dest.LastName, src => src.LastName.Value)
       .Map(dest => dest.Gender, src => src.Gender.Name);
}
