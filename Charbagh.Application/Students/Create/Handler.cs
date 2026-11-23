using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Students.Common;
using Charbagh.Application.Students.Create;
using Charbagh.Domain.Entities.People;
using MediatR;

public sealed class CreateStudentHandler : IRequestHandler<CreateStudentCommand, StudentDto>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IStudentCodeGenerator _studentCodeGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateStudentHandler( IStudentRepository studentRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, IStudentCodeGenerator studentCodeGenerator)
    {
        _studentRepository = studentRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _studentCodeGenerator = studentCodeGenerator;
    }

    public async Task<StudentDto> Handle( CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var existsUser = await _userRepository.ExistsByUserIdAsync( request.UserId, cancellationToken);

        if (!existsUser)
        {
            throw new TeacherNotFoundException();
        }

        var studentCode = await _studentCodeGenerator.GenerateAsync(cancellationToken);

        var student = Student.Create( request.UserId, studentCode, request.RegistrationDate);

        await _studentRepository.AddAsync( student, cancellationToken);

        await _unitOfWork.SaveChangesAsync( cancellationToken);

         return student.ToDto();
    }
}