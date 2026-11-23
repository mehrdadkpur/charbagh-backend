

namespace Charbagh.Application.Branches.Common
{
    public sealed record BranchDto(Guid Id, string Name, string? Code,string? PhoneNumber, string? Address,bool IsActive);
}
