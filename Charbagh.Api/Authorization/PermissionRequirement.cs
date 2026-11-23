using Charbagh.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Charbagh.Api.Authorization;

public sealed class PermissionRequirement : IAuthorizationRequirement
{
    public string Module { get; }

    public PermissionAction Action { get; }

    public PermissionRequirement(
        string module,
        PermissionAction action)
    {
        if (string.IsNullOrWhiteSpace(module))
            throw new ArgumentException(
                "Module is required.",
                nameof(module));

        Module = module.Trim();
        Action = action;
    }
}