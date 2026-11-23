
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Users.Common;
using MediatR;


namespace Charbagh.Application.Users.Update
{
    public sealed class UpdateUserHandler:IRequestHandler<UpdateUserCommand , UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto> Handle(UpdateUserCommand request , CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id , cancellationToken);
            if (user == null)
            {
                throw new UserNotFoundException(request.Id);
            }

            var nationalIdentificationNumberExists = await _userRepository.ExistsNationalIdentificationNumberAsync(request.NationalIdentificationNumber, request.Id, cancellationToken);

            if (nationalIdentificationNumberExists)
            {
                throw new DuplicateNationalityException(
                    request.NationalIdentificationNumber);
            }

            user.Update(
                 request.FirstName,
                 request.LastName,
                 request.Mobile,
                 request.Email,
                 request.NationalityId,
                 request.NationalIdentificationNumber,
                 request.Gender,
                 request.Address,
                 request.BirthDate,
                 request.Bio,
                 request.ProfileImageUrl);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return user.ToDto();
        }
    }
}
