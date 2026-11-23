
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Nationalities.Common;
using MediatR;

namespace Charbagh.Application.Nationalities.GetById
{
    public sealed class GetNationalityByIdHandler : IRequestHandler<GetNationalityByIdQuery , NationalityDto>
    {
        private readonly INationalityRepository _repository;

        public GetNationalityByIdHandler(INationalityRepository nationalityRepository)
        {
            _repository = nationalityRepository;
        }

        public async Task<NationalityDto> Handle (GetNationalityByIdQuery request , CancellationToken cancellationToken)
        {
            var nationality = await _repository.GetByIdAsync(request.Id , cancellationToken);

            if( nationality == null )
            {
                throw new NationalityNotFoundException(request.Id);
            }

            return nationality.ToDto();
        }
    }
}
