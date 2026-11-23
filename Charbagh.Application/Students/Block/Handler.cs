
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Students.Common;
using MediatR;

namespace Charbagh.Application.Students.Block;

public sealed class BlockStudentHandler : IRequestHandler<BlockStudentCommand , StudentDto>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BlockStudentHandler(IStudentRepository studentRepository , IUnitOfWork unitOfWork)
    {
        _studentRepository = studentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<StudentDto> Handle (BlockStudentCommand request , CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(request.Id, cancellationToken);

        if (student == null)
        {
            throw new TeacherNotFoundException();
        }

        student.Block();
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return student.ToDto();
    }
}
