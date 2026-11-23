
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Attendances.Common;
using MediatR;

namespace Charbagh.Application.Attendances.GetAll
{
    public sealed class GetAllAttendancesHandler: IRequestHandler<GetAllAttendancesQuery ,IReadOnlyList< AttendanceDto>>
    {
        private readonly IAttendanceRepository _repository;

        public GetAllAttendancesHandler(IAttendanceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList< AttendanceDto>> Handle (GetAllAttendancesQuery request , CancellationToken cancellationToken)
        {
            var attendances = await _repository.GetAllAsync(cancellationToken);

            return attendances.Select(x => x.ToDto()).ToList();
        }
    }
}
