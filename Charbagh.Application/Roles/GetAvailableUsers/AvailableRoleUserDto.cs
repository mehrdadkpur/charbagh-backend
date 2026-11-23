public sealed record AvailableRoleUserDto(
    Guid UserBranchId,
    Guid UserId,
    string FirstName,
    string LastName,
    string NationalIdentificationNumber,
    string Mobile,
    string? Email,
    UserStatus Status
);