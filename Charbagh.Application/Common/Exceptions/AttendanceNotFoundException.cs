namespace Charbagh.Application.Common.Exceptions;

public sealed class AttendanceNotFoundException : Exception
{
    public AttendanceNotFoundException(Guid Id)
        : base($"This Attendance {Id} Not Found")
    {
    }
}