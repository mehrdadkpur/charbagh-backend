namespace Charbagh.Application.Common.Exceptions;

public sealed class InvoiceItemNotFoundException : Exception
{
    public InvoiceItemNotFoundException(Guid Id)
        : base($"This InvoiceItem {Id} Not Found")
    {
    }
}