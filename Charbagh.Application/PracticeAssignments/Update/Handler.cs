
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.PracticeAssignments.Common;
using MediatR;

namespace Charbagh.Application.PracticeAssignments.Update
{
    public sealed class SetUpdatePracticeAssignmentHandler : IRequestHandler<UpdatePracticeAssignmentCommand, PracticeAssignmentDto>
    {
        private readonly IPracticeAssignmentRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SetUpdatePracticeAssignmentHandler(IPracticeAssignmentRepository practiceAssignment, IUnitOfWork unitOfWork)
        {
            _repository = practiceAssignment;
            _unitOfWork = unitOfWork;
        }

        public async Task<PracticeAssignmentDto> Handle(UpdatePracticeAssignmentCommand request, CancellationToken cancellationToken)
        {
            var practiceAssignment = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (practiceAssignment == null)
            {
                throw new PracticeAssignmentNotFoundException(request.Id);
            }

            practiceAssignment.Update(
                request.BookItemId,
                request.FromReference,
                request.ToReference,
                request.Instruction,
                request.DueDate
                );
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return practiceAssignment.ToDto();
        }
    }

}
