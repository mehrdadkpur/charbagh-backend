using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.StudentParents.Common;
using MediatR;

namespace Charbagh.Application.StudentParents.Create
{
    public class CreateStudentParentHandler : IRequestHandler<CreateStudentParentCommand, StudentParentDto>
    {
        private readonly IStudentParentRepository _studentParentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IParentRepository _parentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateStudentParentHandler(
            IStudentParentRepository studentParentRepository,
            IStudentRepository studentRepository,
            IParentRepository parentRepository,
            IUnitOfWork unitOfWork)
        {
            _studentParentRepository = studentParentRepository;
            _studentRepository = studentRepository;
            _parentRepository = parentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<StudentParentDto> Handle(
            CreateStudentParentCommand request,
            CancellationToken cancellationToken)
        {
            if (request.StudentId == Guid.Empty)
                throw new StudentParentValidationException("شناسه هنرآموز معتبر نیست.");

            if (request.ParentId == Guid.Empty)
                throw new StudentParentValidationException("شناسه والد یا سرپرست معتبر نیست.");

            if (!Enum.IsDefined(typeof(StudentParentRelationship), request.Relationship))
                throw new StudentParentValidationException("نسبت انتخاب‌شده معتبر نیست.");

            var student = await _studentRepository.GetByIdAsync(request.StudentId, cancellationToken);
            if (student is null)
                throw new StudentNotFoundException();

            var birthDate = student.User?.BirthDate;
            if (!birthDate.HasValue)
                throw new StudentParentValidationException(
                    "برای اتصال والد، ابتدا باید تاریخ تولد هنرآموز ثبت شود.");

            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            if (birthDate.Value > today)
                throw new StudentParentValidationException("تاریخ تولد هنرآموز نمی‌تواند در آینده باشد.");

            var age = today.Year - birthDate.Value.Year;
            if (birthDate.Value > today.AddYears(-age))
                age--;

            if (age >= 18)
                throw new StudentParentValidationException(
                    "اتصال والد یا سرپرست فقط برای هنرآموزان زیر ۱۸ سال مجاز است.");

            var parent = await _parentRepository.GetByIdAsync(request.ParentId, cancellationToken);
            if (parent is null)
                throw new ParentNotFoundException();

            var relationshipExists = await _studentParentRepository.ExistsByStudentAndParentAsync(
                request.StudentId, request.ParentId, cancellationToken);

            if (relationshipExists)
                throw new DuplicateStudentParentException(
                    request.StudentId, request.ParentId, cancellationToken);

            var studentParent = StudentParent.Create(
                request.StudentId, request.ParentId, request.Relationship);

            await _studentParentRepository.AddAsync(studentParent, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return studentParent.ToDto();
        }
    }
}
