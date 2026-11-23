namespace Charbagh.Application.Permissions.Current;

public sealed record CurrentPermissionDto(
    string Module,
    string Action);