
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.PracticeAssignments.Common;
using MediatR;

namespace Charbagh.Application.PracticeAssignments.SetNormal
{
    public sealed class SetNormalPriorityPracticeAssignmentHandler : IRequestHandler<SetNormalPriorityPracticeAssignmentCommand, PracticeAssignmentDto>
    {
        private readonly IPracticeAssignmentRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SetNormalPriorityPracticeAssignmentHandler(IPracticeAssignmentRepository practiceAssignment, IUnitOfWork unitOfWork)
        {
            _repository = practiceAssignment;
            _unitOfWork = unitOfWork;
        }

        public async Task<PracticeAssignmentDto> Handle(SetNormalPriorityPracticeAssignmentCommand request, CancellationToken cancellationToken)
        {
            var practiceAssignment = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (practiceAssignment == null)
            {
                throw new PracticeAssignmentNotFoundException(request.Id);
            }

            practiceAssignment.SetNormalPracticePriority();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return practiceAssignment.ToDto();
        }
    }

}
