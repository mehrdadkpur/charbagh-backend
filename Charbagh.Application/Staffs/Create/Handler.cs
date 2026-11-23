using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Staffs.Common;
using Charbagh.Domain.Entities.People;
using MediatR;

namespace Charbagh.Application.Staffs.Create
{
    public sealed class CreateStaffHandler : IRequestHandler<CreateStaffCommand , StaffDto>
    {
        private readonly IStaffRepository _staffrepository;
        private readonly IUserRepository _userRepository;
        private readonly IStaffPersonnelCodeGenerator _codeGenerator;
        private readonly IUnitOfWork _unitOfWork;

        public CreateStaffHandler(IStaffRepository staffRepository, IUserRepository userRepository, IStaffPersonnelCodeGenerator codeGenerator , IUnitOfWork unitOfWork)
        {
            _staffrepository = staffRepository;
            _userRepository = userRepository;
            _codeGenerator = codeGenerator;
            _unitOfWork = unitOfWork;
        }

        public async Task<StaffDto> Handle(CreateStaffCommand request , CancellationToken cancellationToken)
        {
            var existsUser = await _userRepository.ExistsByUserIdAsync(request.UserId, cancellationToken);

            if (!existsUser)
            {
                throw new UserNotFoundException(request.UserId);
            }

            var existStaff = await _staffrepository.ExistsByUserIdAsync(request.UserId, cancellationToken);

            if (existStaff)
            {
                throw new UserAlreadyStaffException(request.UserId);
            }

            var personnelCode = await _codeGenerator.GenerateAsync(cancellationToken);

            var staff = Staff.Create(request.UserId , personnelCode , request.EmploymentDate , request.EmploymentType);

            await _staffrepository.AddAsync(staff , cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return staff.ToDto();

        }
    }
}
