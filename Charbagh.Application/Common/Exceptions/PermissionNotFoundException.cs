namespace Charbagh.Application.Common.Exceptions;

public sealed class PermissionNotFoundException : Exception
{
    public PermissionNotFoundException(Guid Id)
        : base($"This Permission {Id} Not Found")
    {
    }
}