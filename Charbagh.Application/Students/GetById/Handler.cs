
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Students.Common;
using MediatR;

namespace Charbagh.Application.Students.GetById
{
    public sealed class GetStudentByIdHandler:IRequestHandler<GetStudentByIdQuery , StudentDto>
    {
        private readonly IStudentRepository _studentRepository;

        public GetStudentByIdHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<StudentDto> Handle(GetStudentByIdQuery request , CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.Id, cancellationToken);

            if(student == null)
            {
                throw new TeacherNotFoundException();
            }
            return student.ToDto();
        }
    }
}
