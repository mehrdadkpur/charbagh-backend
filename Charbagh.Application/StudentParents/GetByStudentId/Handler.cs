using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.StudentParents.Common;
using MediatR;

namespace Charbagh.Application.StudentParents.GetByStudentId
{
    public sealed class GetStudentParentByStudentIdHandler : IRequestHandler<GetStudentParentByStudentIdQuery , StudentParentDto>
    {
        private readonly IStudentParentRepository _repository;

        public GetStudentParentByStudentIdHandler(IStudentParentRepository repository)
        {
            _repository = repository;
        }

        public async Task <StudentParentDto> Handle (GetStudentParentByStudentIdQuery request , CancellationToken cancellationToken)
        {
            var studentParent = await _repository.GetByStudentIdAsync(request.studentId, cancellationToken);

            if(studentParent == null)
            {
                throw new StudentParentNotFoundException();
            }

            return studentParent.ToDto();
        }
    }
}
