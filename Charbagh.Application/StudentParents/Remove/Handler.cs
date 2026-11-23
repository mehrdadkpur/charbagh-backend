
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.StudentParents.Remove
{
    public sealed class RemoveStudentParentHandler: IRequestHandler<RemoveStudentParentCommand>
    {
        private readonly IStudentParentRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveStudentParentHandler(IStudentParentRepository repository , IUnitOfWork unitOfWork)
        {
            _repository = repository; 
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RemoveStudentParentCommand request , CancellationToken cancellationToken)
        {
            var studentParent = await _repository.RemoveAsync(request.Id, cancellationToken);

            if (studentParent == null)
            {
                throw new StudentParentNotFoundException();
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
