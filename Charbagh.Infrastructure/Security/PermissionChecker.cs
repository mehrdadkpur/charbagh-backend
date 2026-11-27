using Charbagh.Application.Abstractions.Security;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Security;

public sealed class PermissionChecker : IPermissionChecker
{
    private readonly CharbaghDbContext _context;

    public PermissionChecker(CharbaghDbContext context)
    {
        _context = context;
    }

    public async Task<bool> HasAnyActiveRoleAsync(
        Guid userId,
        Guid branchId,
        CancellationToken cancellationToken)
    {
        if (userId == Guid.Empty || branchId == Guid.Empty)
            return false;

        return await _context.UserBranchRoles
            .AsNoTracking()
            .AnyAsync(
                userBranchRole =>
                    userBranchRole.UserBranch.UserId == userId &&
                    userBranchRole.UserBranch.BranchId == branchId &&
                    userBranchRole.UserBranch.IsActive &&
                    userBranchRole.UserBranch.Branch.IsActive &&
                    userBranchRole.IsActive &&
                    userBranchRole.Role.IsActive,
                cancellationToken);
    }

    public async Task<bool> HasPermissionAsync(
        Guid userId,
        Guid branchId,
        string module,
        PermissionAction action,
        CancellationToken cancellationToken)
    {
        if (userId == Guid.Empty ||
            branchId == Guid.Empty ||
            string.IsNullOrWhiteSpace(module))
        {
            return false;
        }

        return await _context.UserBranchRoles
            .AsNoTracking()
            .AnyAsync(
                userBranchRole =>
                    userBranchRole.UserBranch.UserId == userId &&
                    userBranchRole.UserBranch.BranchId == branchId &&
                    userBranchRole.UserBranch.IsActive &&
                    userBranchRole.UserBranch.Branch.IsActive &&
                    userBranchRole.IsActive &&
                    userBranchRole.Role.IsActive &&
                    userBranchRole.Role.RolePermissions.Any(
                        rolePermission =>
                            rolePermission.Permission.Module == module &&
                            rolePermission.Permission.Action == action),
                cancellationToken);
    }
}