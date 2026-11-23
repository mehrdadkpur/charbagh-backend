using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.PracticeAssignments.Common;
using MediatR;

namespace Charbagh.Application.PracticeAssignments.SetHigh
{
    public sealed class SetHighPriorityPracticeAssignmentHandler : IRequestHandler<SetHighPriorityPracticeAssignmentCommand, PracticeAssignmentDto>
    {
        private readonly IPracticeAssignmentRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SetHighPriorityPracticeAssignmentHandler(IPracticeAssignmentRepository practiceAssignment, IUnitOfWork unitOfWork)
        {
            _repository = practiceAssignment;
            _unitOfWork = unitOfWork;
        }

        public async Task<PracticeAssignmentDto> Handle(SetHighPriorityPracticeAssignmentCommand request, CancellationToken cancellationToken)
        {
            var practiceAssignment = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (practiceAssignment == null)
            {
                throw new PracticeAssignmentNotFoundException(request.Id);
            }

            practiceAssignment.SetHighPracticePriority();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return practiceAssignment.ToDto();
        }
    }

}
