
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Students.Common;
using MediatR;

namespace Charbagh.Application.Students.Activate
{
    public sealed class ActivateStudentHandler: IRequestHandler<ActivateStudentCommand , StudentDto>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ActivateStudentHandler(IStudentRepository studentRepository , IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<StudentDto> Handle(ActivateStudentCommand requst , CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(requst.Id, cancellationToken);

            if (student == null)
            {
                throw new TeacherNotFoundException();
            }

            student.Activate();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return student.ToDto();
        } 
    }
}
