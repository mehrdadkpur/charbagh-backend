using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Identity;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories;

public sealed class RoleManagementRepository : IRoleManagementRepository
{
    private readonly CharbaghDbContext _context;

    public RoleManagementRepository(CharbaghDbContext context)
    {
        _context = context;
    }

    public async Task<Role?> GetDetailsAsync(
        Guid roleId,
        CancellationToken cancellationToken)
    {
        return await _context.Roles
            .Include(x => x.UserBranchRoles)
                .ThenInclude(x => x.UserBranch)
                    .ThenInclude(x => x.User)
            .Include(x => x.UserBranchRoles)
                .ThenInclude(x => x.UserBranch)
                    .ThenInclude(x => x.Branch)
            .Include(x => x.UserBranchRoles)
                .ThenInclude(x => x.AssignedByUser)
            .Include(x => x.RolePermissions)
                .ThenInclude(x => x.Permission)
            .FirstOrDefaultAsync(
                x => x.Id == roleId,
                cancellationToken);
    }

    public Task<UserBranch?> GetUserBranchAsync(
        Guid userBranchId,
        CancellationToken cancellationToken)
    {
        return _context.UserBranches
            .FirstOrDefaultAsync(
                x => x.Id == userBranchId,
                cancellationToken);
    }

    public async Task<IReadOnlyList<UserBranch>> GetAvailableUsersAsync(
    Guid roleId,
    Guid branchId,
    CancellationToken cancellationToken)
    {
        return await _context.UserBranches
            .AsNoTracking()
            .Include(x => x.User)
            .Where(x =>
                x.BranchId == branchId &&
                x.IsActive &&
                !_context.UserBranchRoles.Any(assignment =>
                    assignment.UserBranchId == x.Id &&
                    assignment.RoleId == roleId &&
                    assignment.IsActive))
            .OrderBy(x => x.User.FirstName)
            .ThenBy(x => x.User.LastName)
            .ToListAsync(cancellationToken);
    }


    public Task<UserBranchRole?> GetUserBranchRoleAsync(
        Guid roleId,
        Guid userBranchId,
        CancellationToken cancellationToken)
    {
        return _context.UserBranchRoles
            .FirstOrDefaultAsync(
                x => x.RoleId == roleId &&
                     x.UserBranchId == userBranchId,
                cancellationToken);
    }

    public async Task AddUserBranchRoleAsync(
        UserBranchRole userBranchRole,
        CancellationToken cancellationToken)
    {
        await _context.UserBranchRoles.AddAsync(
            userBranchRole,
            cancellationToken);
    }

    public async Task<IReadOnlyList<Permission>> GetPermissionsAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Permissions
            .AsNoTracking()
            .OrderBy(x => x.Module)
            .ThenBy(x => x.Action)
            .ToListAsync(cancellationToken);
    }
}