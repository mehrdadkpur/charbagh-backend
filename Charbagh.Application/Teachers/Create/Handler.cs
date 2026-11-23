using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Teachers.Common;
using Charbagh.Domain.Entities.People;
using MediatR;

namespace Charbagh.Application.Teachers.Create
{
    public sealed class CreateTeacherHandler
        : IRequestHandler<CreateTeacherCommand, TeacherDto>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITeacherPersonnelCodeGenerator _codeGenerator;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTeacherHandler(
            ITeacherRepository teacherRepository,
            IUserRepository userRepository,
            ITeacherPersonnelCodeGenerator codeGenerator,
            IUnitOfWork unitOfWork)
        {
            _teacherRepository = teacherRepository;
            _userRepository = userRepository;
            _codeGenerator = codeGenerator;
            _unitOfWork = unitOfWork;
        }

        public async Task<TeacherDto> Handle(
            CreateTeacherCommand request,
            CancellationToken cancellationToken)
        {
            var existsUser = await _userRepository.ExistsByUserIdAsync(request.UserId, cancellationToken);

            if (!existsUser)
                throw new UserNotFoundException(request.UserId);

            var existsTeacher = await _teacherRepository.ExistsByUserIdAsync(request.UserId, cancellationToken);

            if (existsTeacher)
                throw new UserAlreadyTeacherException(request.UserId);

            var personnelCode = await _codeGenerator.GenerateAsync(cancellationToken);

            var teacher = Teacher.Create(request.UserId, personnelCode, request.EmploymentDate);

            await _teacherRepository.AddAsync(teacher, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var createdTeacher = await _teacherRepository.GetByIdAsync(teacher.Id, cancellationToken);

            if (createdTeacher is null)
                throw new InvalidOperationException("Created teacher could not be found.");

            return createdTeacher.ToDto();
        }
    }
}
