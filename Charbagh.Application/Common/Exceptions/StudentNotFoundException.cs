namespace Charbagh.Application.Common.Exceptions;

public sealed class StudentNotFoundException : Exception
{
    public StudentNotFoundException()
        : base("This Student Not Found")
    {
    }
}