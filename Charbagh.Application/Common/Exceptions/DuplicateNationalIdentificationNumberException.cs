namespace Charbagh.Application.Common.Exceptions;

public sealed class DuplicateNationalIdentificationNumberException : Exception
{
    public DuplicateNationalIdentificationNumberException(string nationalIdentificationNumber)
        : base($"The National Identification Number '{nationalIdentificationNumber}' already exists.")
    {
    }
}