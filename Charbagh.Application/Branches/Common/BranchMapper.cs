using Charbagh.Domain.Entities.Branches;

namespace Charbagh.Application.Branches.Common;

public static class InstrumentMapper
{
    public static BranchDto ToDto(this Branch branch)
    {
        return new BranchDto(
            branch.Id,
            branch.Name,
            branch.Code,
            branch.PhoneNumber,
            branch.Address,
            branch.IsActive);
    }
}