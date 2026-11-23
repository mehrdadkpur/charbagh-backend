using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Authentication.CreateUser;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Domain.Entities.Identity;
using MediatR;

namespace Charbagh.Application.Users.Create;

public sealed class CreateUserHandler
    : IRequestHandler<CreateUserCommand, CreateUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly INationalityRepository _nationalityRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserHandler(
        IUserRepository userRepository,
        INationalityRepository nationalityRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _nationalityRepository = nationalityRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateUserResponse> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var firstName = request.FirstName.Trim();
        var lastName = request.LastName.Trim();
        var mobile = request.Mobile.Trim();
        var nationalIdentificationNumber =
            request.NationalIdentificationNumber.Trim();

        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required.");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required.");

        if (string.IsNullOrWhiteSpace(mobile))
            throw new ArgumentException("Mobile is required.");

        if (string.IsNullOrWhiteSpace(nationalIdentificationNumber))
            throw new ArgumentException(
                "National identification number is required.");

        if (request.NationalityId == Guid.Empty)
            throw new ArgumentException("Nationality is required.");

        if (!Enum.IsDefined(request.Gender))
            throw new ArgumentException("Invalid gender.");

        var mobileExists =
            await _userRepository.ExistsByMobileAsync(
                mobile,
                cancellationToken);

        if (mobileExists)
            throw new DuplicateMobileException(mobile);

        var nationalIdExists =
            await _userRepository.ExistsNationalIdentificationNumberAsync(
                nationalIdentificationNumber,
                null,
                cancellationToken);

        if (nationalIdExists)
            throw new DuplicateNationalIdentificationNumberException(
                nationalIdentificationNumber);

        var nationalityExists =
            await _nationalityRepository.ExistsByIdAsync(
                request.NationalityId,
                cancellationToken);

        if (!nationalityExists)
            throw new InvalidOperationException(
                "The selected nationality does not exist.");

        var user = User.Create(
            firstName: firstName,
            lastName: lastName,
            mobile: mobile,
            email: request.Email?.Trim(),
            nationalityId: request.NationalityId,
            nationalIdentificationNumber:
                nationalIdentificationNumber,
            gender: request.Gender,
            address: request.Address?.Trim(),
            birthDate: request.BirthDate,
            bio: request.Bio?.Trim(),
            profileImageUrl: null);

        await _userRepository.AddAsync(
            user,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return new CreateUserResponse(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Mobile,
            user.NationalityId,
            user.NationalIdentificationNumber,
            user.FullName);
    }
}