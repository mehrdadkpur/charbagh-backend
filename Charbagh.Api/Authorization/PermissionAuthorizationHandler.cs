using System.Security.Claims;
using Charbagh.Application.Abstractions.Security;
using Microsoft.AspNetCore.Authorization;

namespace Charbagh.Api.Authorization;

public sealed class PermissionAuthorizationHandler
    : AuthorizationHandler<PermissionRequirement>
{
    private readonly IPermissionChecker _permissionChecker;
    private readonly ICurrentUser _currentUser;
    private readonly ICurrentBranch _currentBranch;

    public PermissionAuthorizationHandler(
        IPermissionChecker permissionChecker,
        ICurrentUser currentUser,
        ICurrentBranch currentBranch)
    {
        _permissionChecker = permissionChecker;
        _currentUser = currentUser;
        _currentBranch = currentBranch;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        if (!_currentUser.IsAuthenticated)
            return;

        if (_currentUser.UserId == Guid.Empty)
            return;

        if (!_currentBranch.HasBranch)
            return;

        var hasPermission =
            await _permissionChecker.HasPermissionAsync(
                _currentUser.UserId,
                _currentBranch.BranchId,
                requirement.Module,
                requirement.Action,
                CancellationToken.None);

        if (hasPermission)
            context.Succeed(requirement);
    }
}