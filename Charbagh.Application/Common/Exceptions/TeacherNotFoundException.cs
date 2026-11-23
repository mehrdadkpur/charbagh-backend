namespace Charbagh.Application.Common.Exceptions;

public sealed class TeacherNotFoundException : Exception
{
    public TeacherNotFoundException()
        : base("This Teacher Not Found")
    {
    }
}