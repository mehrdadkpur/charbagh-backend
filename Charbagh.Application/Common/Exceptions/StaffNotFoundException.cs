namespace Charbagh.Application.Common.Exceptions;

public sealed class StaffNotFoundException : Exception
{
    public StaffNotFoundException()
        : base("This Staff Not Found")
    {
    }
}