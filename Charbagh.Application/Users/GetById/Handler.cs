using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.Users.GetById;

public sealed class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDetailsDto>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDetailsDto> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetDetailsByIdAsync(
            request.Id,
            cancellationToken);

        if (user is null)
            throw new UserNotFoundException(request.Id);

        return user.ToDetailsDto();
    }
}
