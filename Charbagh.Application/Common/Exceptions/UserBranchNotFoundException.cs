namespace Charbagh.Application.Common.Exceptions;

public sealed class UserBranchNotFoundException : Exception
{
    public UserBranchNotFoundException(Guid Id)
        : base($"This User Branch {Id} Not Found")
    {
    }
}