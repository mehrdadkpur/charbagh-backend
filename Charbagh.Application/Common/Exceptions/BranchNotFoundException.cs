namespace Charbagh.Application.Common.Exceptions;

public sealed class BranchNotFoundException : Exception
{
    public BranchNotFoundException(Guid id)
        : base("This Branch Not Found")
    {
    }
}