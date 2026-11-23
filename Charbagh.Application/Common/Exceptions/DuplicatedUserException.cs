namespace Charbagh.Application.Common.Exceptions;

public sealed class DuplicatedUserException : Exception
{
    public DuplicatedUserException(string? PeopleNationalCode , CancellationToken cancellationToken)
        : base($"The User With National Code '{PeopleNationalCode}' already exists.")
    {
    }
}