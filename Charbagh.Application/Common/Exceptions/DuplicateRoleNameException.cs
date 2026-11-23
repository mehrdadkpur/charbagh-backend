namespace Charbagh.Application.Common.Exceptions;

public sealed class DuplicateRoleNameException : Exception
{
    public DuplicateRoleNameException(string RoleName)
        : base($"The Role '{RoleName}' already exists.")
    {
    }
}