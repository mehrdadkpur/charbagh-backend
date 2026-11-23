using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Nationalities.Common;
using MediatR;

namespace Charbagh.Application.Nationalities.GetAll
{
    public sealed class GetAllNationalitiesHandler : IRequestHandler<GetAllNationalitesQuery, IReadOnlyList<NationalityDto>>
    {
        private readonly INationalityRepository _repository;

        public GetAllNationalitiesHandler(INationalityRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<NationalityDto>> Handle (GetAllNationalitesQuery request , CancellationToken cancellationToken)
        {
            var nationalites = await _repository.GetAllAsync(cancellationToken);

            return nationalites
                .Select(x => x.ToDto())
                .ToList();
        }
    }
}
