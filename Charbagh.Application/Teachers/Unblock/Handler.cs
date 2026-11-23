using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Teachers.Block;
using Charbagh.Application.Teachers.Common;
using MediatR;

namespace Charbagh.Application.Teachers.Unblock
{
    public sealed class UnblockTeacherHandler : IRequestHandler<UnblockTeacherCommand, TeacherDto>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UnblockTeacherHandler(ITeacherRepository teacherRepository, IUnitOfWork unitOfWork)
        {
            _teacherRepository = teacherRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TeacherDto> Handle(UnblockTeacherCommand requst, CancellationToken cancellationToken)
        {
            var teacher = await _teacherRepository.GetByIdAsync(requst.Id, cancellationToken);

            if (teacher == null)
            {
                throw new TeacherNotFoundException();
            }

            teacher.Unblock();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return teacher.ToDto();
        }
    }

}
