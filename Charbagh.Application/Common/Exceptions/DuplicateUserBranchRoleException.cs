namespace Charbagh.Application.Common.Exceptions;

public sealed class DuplicateUserBranchRoleException : Exception
{
    public DuplicateUserBranchRoleException(Guid UserBranchId, Guid RoleId )
        : base($"The User Branch Role With UserBranch Id '{UserBranchId}' , Role Id '{RoleId }' already exists.")
    {
    }
}