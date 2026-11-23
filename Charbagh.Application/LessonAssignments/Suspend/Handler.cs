
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.LessonAssignments.Common;
using MediatR;

namespace Charbagh.Application.LessonAssignments.Suspend
{
    public sealed class SuspendLessonAssignmentHandler : IRequestHandler<SuspendLessonAssignmentCommand, LessonAssignmentDto>
    {
        private readonly ILessonAssignmentRepository _lessonAssignmentrepository;
        private readonly IUnitOfWork _unitOfWork;

        public SuspendLessonAssignmentHandler(ILessonAssignmentRepository lessonAssignmentRepository, IUnitOfWork unitOfWork)
        {

            _lessonAssignmentrepository = lessonAssignmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<LessonAssignmentDto> Handle(SuspendLessonAssignmentCommand request, CancellationToken cancellationToken)
        {
            var lessonAssignment = await _lessonAssignmentrepository.GetByIdAsync(request.Id, cancellationToken);

            if (lessonAssignment == null)
            {
                throw new LessonAssignmentNotFoundException(request.Id);
            }

            lessonAssignment.Suspend();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return lessonAssignment.ToDto();
        }
    }

}
