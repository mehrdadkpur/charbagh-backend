namespace Charbagh.Application.Common.Exceptions;

public sealed class RoleNotFoundException : Exception
{
    public RoleNotFoundException(Guid Id)
        : base($"This Role {Id} Not Found")
    {
    }
}