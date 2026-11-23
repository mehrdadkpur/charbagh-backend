namespace Charbagh.Application.Common.Exceptions;

public sealed class TeacherInstrumentNotFoundException : Exception
{
    public TeacherInstrumentNotFoundException(Guid Id)
        : base($"This Teacher Instrument {Id} Not Found")
    {
    }
}