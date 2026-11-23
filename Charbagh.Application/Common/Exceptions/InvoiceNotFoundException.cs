namespace Charbagh.Application.Common.Exceptions;

public sealed class InvoiceNotFoundException : Exception
{
    public InvoiceNotFoundException(Guid Id)
        : base($"This Invoice {Id} Not Found")
    {
    }
}