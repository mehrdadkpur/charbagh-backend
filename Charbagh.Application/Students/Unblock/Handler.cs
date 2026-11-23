
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Students.Common;

using MediatR;

namespace Charbagh.Application.Students.Unblock
{
    public sealed class UnblockStudentHandler:IRequestHandler<UnblockStudentCommand , StudentDto>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UnblockStudentHandler(IStudentRepository studentRepository , IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<StudentDto> Handle(UnblockStudentCommand request , CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.Id, cancellationToken);
            if (student == null)
            {
                throw new StudentNotFoundException();
            }
            if (student.Status == StudentStatus.Blocked)
            {
                student.Unblock();
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            return student.ToDto();
        }
    }
}
