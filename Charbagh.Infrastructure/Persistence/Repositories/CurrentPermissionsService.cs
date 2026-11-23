using Charbagh.Application.Abstractions.Security;
using Charbagh.Application.Common.Interfaces;
using Charbagh.Application.Permissions.Current;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Services;

public sealed class CurrentPermissionsService : ICurrentPermissionsService
{
    private readonly CharbaghDbContext _context;
    private readonly ICurrentUser _currentUser;
    private readonly ICurrentBranch _currentBranch;

    public CurrentPermissionsService(
        CharbaghDbContext context,
        ICurrentUser currentUser,
        ICurrentBranch currentBranch)
    {
        _context = context;
        _currentUser = currentUser;
        _currentBranch = currentBranch;
    }



    public async Task<CurrentPermissionsDto?> GetCurrentAsync( CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var branchId = _currentBranch.BranchId;

        Console.WriteLine( $"[CurrentPermissions] UserId: {userId}");

        Console.WriteLine( $"[CurrentPermissions] BranchId: {branchId}");

        if (userId == Guid.Empty || branchId == Guid.Empty)
        {
            Console.WriteLine(
                "[CurrentPermissions] UserId or BranchId is empty.");

            return null;
        }

        if (userId == Guid.Empty || branchId == Guid.Empty)
            return null;

        var hasActiveMembership = await _context.UserBranches
            .AsNoTracking()
            .AnyAsync(
                userBranch =>
                    userBranch.UserId == userId &&
                    userBranch.BranchId == branchId &&
                    userBranch.IsActive &&
                    userBranch.Branch.IsActive,
                cancellationToken);

        if (!hasActiveMembership)
            return null;

        Console.WriteLine( $"[CurrentPermissions] HasActiveMembership: {hasActiveMembership}");

        if (!hasActiveMembership)
            return null;

        var permissions = await _context.UserBranchRoles
            .AsNoTracking()
            .Where(userBranchRole =>
                userBranchRole.UserBranch.UserId == userId &&
                userBranchRole.UserBranch.BranchId == branchId &&
                userBranchRole.UserBranch.IsActive &&
                userBranchRole.UserBranch.Branch.IsActive &&
                userBranchRole.IsActive &&
                userBranchRole.Role.IsActive)
            .SelectMany(userBranchRole =>
                userBranchRole.Role.RolePermissions)
            .Select(rolePermission => new
            {
                rolePermission.Permission.Module,
                Action = (int)rolePermission.Permission.Action
            })
            .Distinct()
            .OrderBy(permission => permission.Module)
            .ThenBy(permission => permission.Action)
            .ToListAsync(cancellationToken);

        var result = permissions
            .Select(permission => new CurrentPermissionDto(
                permission.Module,
                Enum.GetName(
                    typeof(PermissionAction),
                    permission.Action)
                    ?? permission.Action.ToString()))
            .ToArray();

        return new CurrentPermissionsDto(
            branchId,
            result);
    }
}