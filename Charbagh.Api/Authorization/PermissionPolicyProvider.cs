using Charbagh.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Charbagh.Api.Authorization;

public sealed class PermissionPolicyProvider: DefaultAuthorizationPolicyProvider
{
    private const string PolicyPrefix = "Permission:";

    public PermissionPolicyProvider(IOptions<AuthorizationOptions> options): base(options)
    {
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync( string policyName)
    {
        if (!policyName.StartsWith(PolicyPrefix,StringComparison.OrdinalIgnoreCase))
        {
            return await base.GetPolicyAsync(policyName);
        }

        var permissionParts = policyName[PolicyPrefix.Length..].Split(':', 2);

        if (permissionParts.Length != 2 ||
            string.IsNullOrWhiteSpace(permissionParts[0]) ||
            !Enum.TryParse<PermissionAction>(
                permissionParts[1],
                ignoreCase: true,
                out var action) ||
            !Enum.IsDefined(action))
        {
            return null;
        }

        var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .AddRequirements(
                new PermissionRequirement(
                    permissionParts[0],
                    action))
            .Build();

        return policy;
    }
}