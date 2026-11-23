using Charbagh.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Charbagh.Api.Authorization;

[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Method,
    AllowMultiple = true,
    Inherited = true)]
public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(
        string module,
        PermissionAction action)
    {
        if (string.IsNullOrWhiteSpace(module))
            throw new ArgumentException(
                "Module is required.",
                nameof(module));

        Policy = $"Permission:{module.Trim()}:{action}";
    }
}