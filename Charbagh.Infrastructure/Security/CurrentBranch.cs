using Charbagh.Application.Abstractions.Security;
using Microsoft.AspNetCore.Http;

namespace Charbagh.Infrastructure.Security;

public sealed class CurrentBranch : ICurrentBranch
{
    private const string BranchHeaderName = "X-Branch-Id";

    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentBranch(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid BranchId
    {
        get
        {
            var value = _httpContextAccessor
                .HttpContext?
                .Request
                .Headers[BranchHeaderName]
                .FirstOrDefault();

            return Guid.TryParse(value, out var branchId)
                ? branchId
                : Guid.Empty;
        }
    }

    public bool HasBranch => BranchId != Guid.Empty;
}