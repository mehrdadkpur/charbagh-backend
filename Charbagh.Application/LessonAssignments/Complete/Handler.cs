using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.LessonAssignments.Common;
using MediatR;

namespace Charbagh.Application.LessonAssignments.Complete
{
    public sealed class CompleteLessonAssignmentHandler : IRequestHandler<CompleteLessonAssignmentCommand, LessonAssignmentDto>
    {
        private readonly ILessonAssignmentRepository _lessonAssignmentrepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompleteLessonAssignmentHandler(ILessonAssignmentRepository lessonAssignmentRepository, IUnitOfWork unitOfWork)
        {

            _lessonAssignmentrepository = lessonAssignmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<LessonAssignmentDto> Handle(CompleteLessonAssignmentCommand request, CancellationToken cancellationToken)
        {
            var lessonAssignment = await _lessonAssignmentrepository.GetByIdAsync(request.Id, cancellationToken);

            if (lessonAssignment == null)
            {
                throw new LessonAssignmentNotFoundException(request.Id);
            }

            lessonAssignment.Complete(request.EndDate);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return lessonAssignment.ToDto();
        }
    }

}
