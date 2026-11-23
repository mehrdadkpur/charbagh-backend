namespace Charbagh.Application.Common.Exceptions;

public sealed class StudentParentNotFoundException : Exception
{
    public StudentParentNotFoundException()
        : base("This StudentParent Not Found")
    {
    }
}