
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.PracticeAssignments.Common;
using Charbagh.Application.PracticeAssignments.SetHigh;
using MediatR;

namespace Charbagh.Application.PracticeAssignments.SetLow
{
    public sealed class SetLowPriorityPracticeAssignmentHandler : IRequestHandler<SetLowPriorityPracticeAssignmentCommand, PracticeAssignmentDto>
    {
        private readonly IPracticeAssignmentRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SetLowPriorityPracticeAssignmentHandler(IPracticeAssignmentRepository practiceAssignment, IUnitOfWork unitOfWork)
        {
            _repository = practiceAssignment;
            _unitOfWork = unitOfWork;
        }

        public async Task<PracticeAssignmentDto> Handle(SetLowPriorityPracticeAssignmentCommand request, CancellationToken cancellationToken)
        {
            var practiceAssignment = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (practiceAssignment == null)
            {
                throw new PracticeAssignmentNotFoundException(request.Id);
            }

            practiceAssignment.SetLowPracticePriority();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return practiceAssignment.ToDto();
        }
    }

}
