
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.LessonAssignments.Common;
using MediatR;

namespace Charbagh.Application.LessonAssignments.Update
{
    public sealed class UpdateLessonAssignmentHandler : IRequestHandler<UpdateLessonAssignmentCommand, LessonAssignmentDto>
    {
        private readonly ILessonAssignmentRepository _lessonAssignmentrepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLessonAssignmentHandler(ILessonAssignmentRepository lessonAssignmentRepository, IUnitOfWork unitOfWork)
        {

            _lessonAssignmentrepository = lessonAssignmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<LessonAssignmentDto> Handle(UpdateLessonAssignmentCommand request, CancellationToken cancellationToken)
        {
            var lessonAssignment = await _lessonAssignmentrepository.GetByIdAsync(request.Id, cancellationToken);

            if (lessonAssignment == null)
            {
                throw new LessonAssignmentNotFoundException(request.Id);
            }

            var existsActiveAssignment = await _lessonAssignmentrepository.ExistsActiveAssignmentAsync(request.StudentId , request.InstrumentId , request.Id, cancellationToken);

            if (existsActiveAssignment)
            {
                throw new DuplicateLessonAssignmentException(request.StudentId, request.InstrumentId);
            }

            lessonAssignment.Update(request.TeacherId , request.BookId , request.SessionFee , request.PaymentPlan , request.Notes);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return lessonAssignment.ToDto();
        }
    }

}
