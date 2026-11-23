using Charbagh.Application.TeacherInstruments.Common;
using MediatR;

namespace Charbagh.Application.TeacherInstruments.GetByInstrumentId;

public sealed record GetTeacherInstrumentsByInstrumentIdQuery( Guid InstrumentId) : IRequest<IReadOnlyList<TeacherInstrumentDto>>;