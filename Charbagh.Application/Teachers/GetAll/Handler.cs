using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Teachers.Common;
using MediatR;

namespace Charbagh.Application.Teachers.GetAll
{
    public sealed class GetAllTeachersHandler: IRequestHandler<GetAllTeachersQuery , IReadOnlyList<TeacherDto>>
    {
        private readonly ITeacherRepository _repository;

        public GetAllTeachersHandler(ITeacherRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<TeacherDto>> Handle(GetAllTeachersQuery request , CancellationToken cancellationToken)
        {
            var teachers = await _repository.GetAllAsync(cancellationToken);

            return teachers
                .Select(x => x.ToDto())
                .ToList();
        }
       
    }
}
