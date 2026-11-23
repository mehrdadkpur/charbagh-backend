namespace Charbagh.Application.Common.Exceptions;

public sealed class ParentNotFoundException : Exception
{
    public ParentNotFoundException()
        : base("This Parent Not Found")
    {
    }
}