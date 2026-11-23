
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.PracticeAssignments.Common;
using MediatR;

namespace Charbagh.Application.PracticeAssignments.GetById
{
    public sealed class GetPracticeAssignmentHandler: IRequestHandler<GetPracticeAssignmentByIdQuery , PracticeAssignmentDto>
    {
        private readonly IPracticeAssignmentRepository _repository;

        public GetPracticeAssignmentHandler(IPracticeAssignmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<PracticeAssignmentDto> Handle(GetPracticeAssignmentByIdQuery request , CancellationToken cancellationToken)
        {
            var practiceAssignment = await _repository.GetByIdAsync(request.Id , cancellationToken);

            if (practiceAssignment == null)
            {
                throw new PracticeAssignmentNotFoundException(request.Id);
            }

            return practiceAssignment.ToDto();
        }
    }
}
