namespace Charbagh.Application.Common.Exceptions;

public sealed class UserBranchRoleNotFoundException : Exception
{
    public UserBranchRoleNotFoundException(Guid Id)
        : base($"This User Branch Role {Id} Not Found")
    {
    }
}