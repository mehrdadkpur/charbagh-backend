namespace Charbagh.Application.Common.Exceptions;

public sealed class DuplicatePermissionException : Exception
{
    public DuplicatePermissionException(string module , PermissionAction action)
        : base($"The Permission With Moudle {module} And Permission Action '{action}' already exists.")
    {
    }
}