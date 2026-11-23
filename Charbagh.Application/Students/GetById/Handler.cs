
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Students.Common;
using MediatR;

namespace Charbagh.Application.Students.GetById
{
    public sealed class GetStudentByIdHandler : IRequestHandler<GetStudentByIdQuery, StudentDetailsDto>
    {
        private readonly IStudentRepository _studentRepository;

        public GetStudentByIdHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<StudentDetailsDto> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetDetailsByIdAsync(request.Id, cancellationToken);

            if (student is null)
                throw new StudentNotFoundException();

            return student.ToDetailsDto();
        }
    }
}
