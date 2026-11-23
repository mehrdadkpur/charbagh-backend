namespace Charbagh.Application.Common.Exceptions;

public sealed class InstrumentCategoryNotFoundException : Exception
{
    public InstrumentCategoryNotFoundException(Guid id)
        : base($"Instrument category with id '{id}' was not found.")
    {
    }
}