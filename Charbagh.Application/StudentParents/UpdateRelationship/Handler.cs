
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.StudentParents.Common;
using MediatR;

namespace Charbagh.Application.StudentParents.UpdateRelationship
{
    public sealed class UpdateStudentParentRelationHandler: IRequestHandler<UpdateStudentParentRelationCommand , StudentParentDto>
    {
        private readonly IStudentParentRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStudentParentRelationHandler(IStudentParentRepository studentParentRepository , IUnitOfWork unitOfWork)
        {
            _repository = studentParentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<StudentParentDto> Handle (UpdateStudentParentRelationCommand request , CancellationToken cancellationToken)
        {
            var studentParent = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (studentParent == null)
            {
                throw new StudentParentNotFoundException();
            }

            studentParent.UpdateRelationship(request.Relationship);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return studentParent.ToDto();
        }
    }
}
