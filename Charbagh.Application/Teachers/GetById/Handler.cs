
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Teachers.Common;
using MediatR;

namespace Charbagh.Application.Teachers.GetById
{
    public sealed class GetTeacherByIdHandler:IRequestHandler<GetTeacherByIdQuery , TeacherDto>
    {
        private readonly ITeacherRepository _teacherRepository;

        public GetTeacherByIdHandler(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<TeacherDto> Handle(GetTeacherByIdQuery request , CancellationToken cancellationToken)
        {
            var teacher = await _teacherRepository.GetByIdAsync(request.Id, cancellationToken);

            if(teacher == null)
            {
                throw new TeacherNotFoundException();
            }
            return teacher.ToDto();
        }
    }
}
