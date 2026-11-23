using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.RefreshTokens.Common;
using MediatR;

namespace Charbagh.Application.RefreshTokens.GetById
{
    public sealed class GetRefreshTokenByIdHandler : IRequestHandler<GetRefreshTokenByIdQuery,RefreshTokenDto>
    {
        private readonly IRefreshTokenRepository _repository;
        
        public GetRefreshTokenByIdHandler(IRefreshTokenRepository repository)
        {
            _repository = repository;
        }   
        public async Task<RefreshTokenDto> Handle(GetRefreshTokenByIdQuery request , CancellationToken cancellationToken)
        {
            var refreshToken = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (refreshToken == null)
            {
                throw new RefreshTokenNotFoundException(request.Id);
            }

            return refreshToken.ToDto();
        }
    }
}
