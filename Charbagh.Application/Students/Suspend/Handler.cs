using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Students.Common;
using MediatR;


namespace Charbagh.Application.Students.Suspend;

public sealed class SuspendUserHandler : IRequestHandler<SuspendStudentCommand , StudentDto>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SuspendUserHandler(IStudentRepository studentRepository , IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _studentRepository = studentRepository;
    }

    public async Task<StudentDto> Handle (SuspendStudentCommand request , CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(request.Id, cancellationToken);

        if(student == null)
        {
            throw new TeacherNotFoundException();
        }

        if(student.Status == StudentStatus.Active)
        {
            student.Suspend();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        return student.ToDto();
    }
}

