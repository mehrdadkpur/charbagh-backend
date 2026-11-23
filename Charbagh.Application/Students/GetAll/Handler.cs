using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Students.Common;
using Charbagh.Application.Students.GetAll;
using MediatR;

namespace Charbagh.Application.Students.GetAll
{
    public sealed class GetAllStudentsHandler : IRequestHandler<GetAllStudentsQuery, IReadOnlyList<StudentDto>>
    {
        private readonly IStudentRepository _studentRepository;

        public GetAllStudentsHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IReadOnlyList<StudentDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentRepository.GetAllAsync(cancellationToken);

            return students
                .Select(x => x.ToDto())
                .ToList();
        }
    }
}
