
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Students.Common;
using MediatR;

namespace Charbagh.Application.Students.Update
{
    public sealed class UpdateStudentHandler: IRequestHandler<UpdateStudentCommand , StudentDto>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStudentHandler(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<StudentDto> Handle(UpdateStudentCommand request , CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.Id, cancellationToken);

            if (student == null)
            {
                throw new TeacherNotFoundException();
            }

            var existsStudent = await _studentRepository.ExistsByUserIdExceptAsync(request.UserId,request.Id , cancellationToken);

            if (existsStudent)
            {
                throw new UserAlreadyStudentException(request.UserId);
            }

            var existsStudentCode = await _studentRepository.ExistsByStudentCodeExceptAsync(request.StudentCode, request.Id , cancellationToken);

            if (existsStudentCode)
            {
                throw new DuplicateStudentCodeException(request.StudentCode);
            }

            student.Update( request.StudentCode, request.RegistrationDate , request.Notes );

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return student.ToDto();

        }
    }
}
