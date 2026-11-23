
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.PracticeAssignments.Common;
using MediatR;

namespace Charbagh.Application.PracticeAssignments.GetAll
{
    public sealed class GetAllPracticeAssignmentsHandler : IRequestHandler<GetAllPracticeAssignmentsQuery, IReadOnlyList<PracticeAssignmentDto>>
    {
        private readonly IPracticeAssignmentRepository _repository;

        public GetAllPracticeAssignmentsHandler(IPracticeAssignmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList< PracticeAssignmentDto>> Handle(GetAllPracticeAssignmentsQuery request , CancellationToken cancellationToken)
        {
            var practiceAssignmrents = await _repository.GetAllAsync(cancellationToken);

            return practiceAssignmrents.Select(x => x.ToDto()).ToList();
        }
    }
}
