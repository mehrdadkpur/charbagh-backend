namespace Charbagh.Application.Common.Exceptions;

public sealed class DuplicateUserBranchException : Exception
{
    public DuplicateUserBranchException(Guid UserId, Guid BranchId )
        : base($"The User Branch With User Id '{UserId}' , Branch Id '{BranchId}' already exists.")
    {
    }
}