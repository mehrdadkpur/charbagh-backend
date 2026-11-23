namespace Charbagh.Application.Common.Exceptions;

public sealed class DuplicateBranchCodeException : Exception
{
    public DuplicateBranchCodeException(string code)
        : base($"Branch code '{code}' already exists.")
    {
    }
}