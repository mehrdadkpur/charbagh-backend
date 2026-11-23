
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.StudentParents.Common;
using MediatR;

namespace Charbagh.Application.StudentParents.GetById
{
    public sealed class GetStudentParentByIdHandler : IRequestHandler<GetStudentParentByIdQuery , StudentParentDto>
    {
        private readonly IStudentParentRepository _repository;
        
        public GetStudentParentByIdHandler(IStudentParentRepository repository)
        {
            _repository = repository; 
        }

        public async Task<StudentParentDto> Handle (GetStudentParentByIdQuery request , CancellationToken cancellationToken)
        {
            var studentParent = await _repository.GetByIdAsync(request.Id , cancellationToken);

            if (studentParent == null)
            {
                throw new StudentParentNotFoundException();
            }

            return studentParent.ToDto();
        }
    }
}
