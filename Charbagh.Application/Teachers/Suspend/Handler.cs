using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Teachers.Common;
using MediatR;

namespace Charbagh.Application.Teachers.Suspend
{
    public sealed class SuspendTeachertHandler : IRequestHandler<SuspendTeacherCommand, TeacherDto>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SuspendTeachertHandler(ITeacherRepository teacherRepository, IUnitOfWork unitOfWork)
        {
            _teacherRepository = teacherRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TeacherDto> Handle(SuspendTeacherCommand requst, CancellationToken cancellationToken)
        {
            var teacher = await _teacherRepository.GetByIdAsync(requst.Id, cancellationToken);

            if (teacher == null)
            {
                throw new TeacherNotFoundException();
            }

            teacher.Suspend();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return teacher.ToDto();
        }
    }

}
