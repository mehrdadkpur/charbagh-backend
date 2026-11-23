using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.LessonAssignments.Common;
using MediatR;

namespace Charbagh.Application.LessonAssignments.GetAll
{
    public sealed class GetAllLessonAssignmentsHandler : IRequestHandler<GetAllLessonAssignmentsQuery,IReadOnlyList< LessonAssignmentDto>>
    {
        private readonly ILessonAssignmentRepository _repository;

        public GetAllLessonAssignmentsHandler(ILessonAssignmentRepository repository)
        {

            _repository = repository;
        }

        public async Task<IReadOnlyList< LessonAssignmentDto>> Handle(GetAllLessonAssignmentsQuery request, CancellationToken cancellationToken)
        {
            var lessonAssignment = await _repository.GetAllAsync(cancellationToken);

            return lessonAssignment.Select(x => x.ToDto()).ToList();
        }
    }
}
