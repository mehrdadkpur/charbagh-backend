
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.LessonAssignments.Common;
using MediatR;

namespace Charbagh.Application.LessonAssignments.GetById
{
    public sealed class GetLessonAssignmentByIdHandler : IRequestHandler<GetLessonAssignmentByIdQuery , LessonAssignmentDto>
    {
        private readonly ILessonAssignmentRepository _repository;

        public GetLessonAssignmentByIdHandler(ILessonAssignmentRepository repository)
        {

            _repository = repository;
        }

        public async Task<LessonAssignmentDto> Handle (GetLessonAssignmentByIdQuery request , CancellationToken cancellationToken)
        {
            var lessonAssignment = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (lessonAssignment == null)
            {
                throw new LessonAssignmentNotFoundException(request.Id);
            }

            return lessonAssignment.ToDto();
        }
    }
}
