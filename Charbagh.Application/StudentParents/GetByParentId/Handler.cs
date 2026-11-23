
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.StudentParents.Common;
using MediatR;

namespace Charbagh.Application.StudentParents.GetByParentId
{
    public sealed class GetStudentParentByParentIdHandler : IRequestHandler<GetStudentParentByParentIdQuery , StudentParentDto>
    {
        private readonly IStudentParentRepository _repository;

        public GetStudentParentByParentIdHandler(IStudentParentRepository repository)
        {
            _repository = repository;
        }

        public async Task<StudentParentDto> Handle (GetStudentParentByParentIdQuery request, CancellationToken cancellationToken)
        {
            var studentParent = await _repository.GetByParentIdAsync(request.ParentId, cancellationToken);

            if (studentParent == null)
            {
                throw new ParentNotFoundException();
            }

            return studentParent.ToDto();
        }
    }
}
