using Application.Common.DTOs.Users;
using Application.Users.Queries.GetUserById;
using MediatR;

namespace Application.Users.Queries.GetUserByIdString;

internal sealed class GetUserByIdStringQueryHandler(ISender sender) : 
    IQueryHandler<GetUserByIdStringQuery, UserDto>
{
    private readonly ISender _sender = sender;

    public async Task<Result<UserDto>> Handle(GetUserByIdStringQuery request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(request.Id);
        var query = new GetUserByIdQuery(userId);
        return await _sender.Send(query, cancellationToken);
    }
}
