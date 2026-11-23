
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.LessonAssignments.Common;
using MediatR;

namespace Charbagh.Application.LessonAssignments.GetByStudentId
{
    public sealed class GetLessonAssignmentByStudentIdHandler : IRequestHandler<GetLessonAssignmentByStudentIdQuery , LessonAssignmentDto>
    {
        private readonly ILessonAssignmentRepository _repository;

        public GetLessonAssignmentByStudentIdHandler(ILessonAssignmentRepository repository)
        {

            _repository = repository;
        }

        public async Task<LessonAssignmentDto> Handle(GetLessonAssignmentByStudentIdQuery request, CancellationToken cancellationToken)
        {
            var lessonAssignment = await _repository.GetByStudentIdAsync(request.StudentId, cancellationToken);

            if (lessonAssignment == null)
            {
                throw new LessonAssignmentNotFoundException(request.StudentId);
            }

            return lessonAssignment.ToDto();
        }
    }
}
