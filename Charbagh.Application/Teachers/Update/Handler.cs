using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Teachers.Common;
using MediatR;

namespace Charbagh.Application.Teachers.Update
{
    public sealed class UpdateTeacherHandler : IRequestHandler<UpdateTeacherCommand , TeacherDto>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTeacherHandler(ITeacherRepository teacherRepository , IUnitOfWork unitOfWork)
        {
            _teacherRepository = teacherRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TeacherDto> Handle (UpdateTeacherCommand request , CancellationToken cancellationToken)
        {
            var teacher = await _teacherRepository.GetByIdAsync(request.Id, cancellationToken);

            if (teacher == null)
            {
                throw new TeacherNotFoundException();
            }

            var existTeacher = await _teacherRepository.ExistsByUserIdExceptAsync(request.UserId , request.Id, cancellationToken);

            if (existTeacher)
            {
                throw new UserAlreadyTeacherException(request.UserId);
            }

            teacher.Update(request.EmploymentDate);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return teacher.ToDto();
        }
    }
}
