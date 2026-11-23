
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Attendances.Common;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.Attendances.GetById
{
    public sealed class GetAttendanceByIdHandler : IRequestHandler<GetAttendanceByIdQuery , AttendanceDto>
    {
        private readonly IAttendanceRepository _repository;

        public GetAttendanceByIdHandler(IAttendanceRepository repository)
        {
            _repository = repository;
        }

        public async Task<AttendanceDto> Handle (GetAttendanceByIdQuery request , CancellationToken cancellationToken)
        {
            var attendance = await _repository.GetByIdAsync(request.Id , cancellationToken);

            if(attendance == null)
            {
                throw new AttendanceNotFoundException(request.Id);
            }

            return attendance.ToDto();
        }
    }
}
