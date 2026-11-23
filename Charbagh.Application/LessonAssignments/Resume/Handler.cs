
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.LessonAssignments.Common;
using MediatR;

namespace Charbagh.Application.LessonAssignments.Resume
{
    public sealed class ResumeLessonAssignmentHandler : IRequestHandler<ResumeLessonAssignmentCommand, LessonAssignmentDto>
    {
        private readonly ILessonAssignmentRepository _lessonAssignmentrepository;
        private readonly IUnitOfWork _unitOfWork;

        public ResumeLessonAssignmentHandler(ILessonAssignmentRepository lessonAssignmentRepository, IUnitOfWork unitOfWork)
        {

            _lessonAssignmentrepository = lessonAssignmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<LessonAssignmentDto> Handle(ResumeLessonAssignmentCommand request, CancellationToken cancellationToken)
        {
            var lessonAssignment = await _lessonAssignmentrepository.GetByIdAsync(request.Id, cancellationToken);

            if (lessonAssignment == null)
            {
                throw new LessonAssignmentNotFoundException(request.Id);
            }

            lessonAssignment.Resume();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return lessonAssignment.ToDto();
        }
    }
}

