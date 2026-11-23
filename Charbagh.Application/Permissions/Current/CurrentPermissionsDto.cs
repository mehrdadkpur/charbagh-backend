namespace Charbagh.Application.Permissions.Current;

public sealed record CurrentPermissionsDto(
    Guid BranchId,
    IReadOnlyCollection<CurrentPermissionDto> Permissions);