using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.StudentParents.Common;
using MediatR;

namespace Charbagh.Application.StudentParents.Create
{
    public class CreateStudentParentHandler : IRequestHandler<CreateStudentParentCommand , StudentParentDto>
    {
        private readonly IStudentParentRepository _studentParentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IParentRepository _parentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateStudentParentHandler(IStudentParentRepository studentParentRepository, IStudentRepository studentRepository, IParentRepository parentRepository , IUnitOfWork unitOfWork)
        {
            _studentParentRepository = studentParentRepository;
            _studentRepository = studentRepository;
            _parentRepository = parentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<StudentParentDto> Handle(CreateStudentParentCommand request , CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.StudentId, cancellationToken);

            if (student == null)
            {
                throw new StudentNotFoundException();
            }

            var parent = await _parentRepository.GetByIdAsync(request.ParentId, cancellationToken);

            if (parent == null)
            {
                throw new ParentNotFoundException();
            }

            var relationshipExists = await _studentParentRepository.ExistsByStudentAndParentAsync( request.StudentId, request.ParentId,cancellationToken);

            if (relationshipExists)
            {
                throw new DuplicateStudentParentException( request.StudentId, request.ParentId , cancellationToken);
            }

            var studentParent = StudentParent.Create(request.StudentId, request.ParentId , request.Relationship);
            await _studentParentRepository.AddAsync(studentParent , cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return studentParent.ToDto();
        }
    }
}
