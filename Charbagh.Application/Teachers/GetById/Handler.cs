
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Teachers.Common;
using MediatR;

namespace Charbagh.Application.Teachers.GetById
{
    public sealed class GetTeacherByIdHandler : IRequestHandler<GetTeacherByIdQuery, TeacherDetailsDto>
    {
        private readonly ITeacherRepository _teacherRepository;

        public GetTeacherByIdHandler(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<TeacherDetailsDto> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
        {
            var teacher = await _teacherRepository.GetDetailsByIdAsync(request.Id, cancellationToken);

            if (teacher is null)
                throw new TeacherNotFoundException();

            return teacher.ToDetailsDto();
        }
    }
}
