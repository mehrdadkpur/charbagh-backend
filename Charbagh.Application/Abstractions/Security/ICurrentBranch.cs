namespace Charbagh.Application.Abstractions.Security;

public interface ICurrentBranch
{
    Guid BranchId { get; }

    bool HasBranch { get; }
}