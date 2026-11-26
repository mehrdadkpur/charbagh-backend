using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.PracticeAssignments.Common;
using Charbagh.Domain.Entities.Education.StudentEducation;
using MediatR;

namespace Charbagh.Application.PracticeAssignments.Create
{
    public sealed class CreatePracticeAssignmentHandler : IRequestHandler<CreatePracticeAssignmentCommand, PracticeAssignmentDto>
    {
        private readonly IPracticeAssignmentRepository _practiceAssignmentRepository ;
        private readonly ILessonSessionRepository _lessonSessionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePracticeAssignmentHandler(IPracticeAssignmentRepository practiceAssignmentRepository, ILessonSessionRepository lessonSessionRepository, IUnitOfWork unitOfWork)
        {
            _practiceAssignmentRepository = practiceAssignmentRepository;
            _lessonSessionRepository = lessonSessionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PracticeAssignmentDto> Handle(CreatePracticeAssignmentCommand request, CancellationToken cancellationToken)
        {
            var lessonSession = await _lessonSessionRepository.GetByIdAsync(request.LessonSessionId, cancellationToken);

            if (lessonSession == null)
            {
                throw new LessonSessionNotFoundException(request.LessonSessionId);
            }

            var practiceAssignment = PracticeAssignment.Create(
                request.LessonSessionId,
                request.BookItemId,
                request.FromReference,
                request.ToReference,
                request.Instruction,
                request.DueDate,
                request.Priority
                );

            await _practiceAssignmentRepository.AddAsync(practiceAssignment, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return practiceAssignment.ToDto();
        }
    }

}
