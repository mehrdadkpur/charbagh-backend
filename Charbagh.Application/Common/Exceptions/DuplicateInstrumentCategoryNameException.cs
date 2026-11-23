namespace Charbagh.Application.Common.Exceptions;

public sealed class DuplicateInstrumentCategoryNameException : Exception
{
    public DuplicateInstrumentCategoryNameException(string name)
        : base($"Instrument category with name '{name}' already exists.")
    {
    }
}