using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.LessonAssignments.Common;
using Charbagh.Domain.Entities.Education.StudentEducation;
using MediatR;

namespace Charbagh.Application.LessonAssignments.Create
{
    public sealed class CreateLessonAssignmentHandler : IRequestHandler<CreateLessonAssignmentCommand , LessonAssignmentDto>
    {
        private readonly ILessonAssignmentRepository _lessonAssignmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateLessonAssignmentHandler(ILessonAssignmentRepository lessonAssignmentRepository , IUnitOfWork unitOfWork)
        {
            _lessonAssignmentRepository = lessonAssignmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<LessonAssignmentDto> Handle(CreateLessonAssignmentCommand request , CancellationToken cancellationToken)
        {
            var existsActiveAssignment = await _lessonAssignmentRepository.ExistsActiveAssignmentAsync( request.StudentId, request.InstrumentId, null,cancellationToken);

            if (existsActiveAssignment)
            {
                throw new DuplicateLessonAssignmentException( request.StudentId, request.InstrumentId);
            }

            var lessonAssignment = LessonAssignment.Create(
                request.StudentId,
                request.BookId,
                request.TeacherId,
                request.InstrumentId,
                request.StartDate,
                request.SessionFee,
                request.PaymentPlan,
                request.Notes
                );

            await _lessonAssignmentRepository.AddAsync(lessonAssignment, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return lessonAssignment.ToDto();
        }
    }
}
