using Charbagh.Application.TeacherInstruments.Common;
using MediatR;

namespace Charbagh.Application.TeacherInstruments.GetByTeacherId;

public sealed record GetTeacherInstrumentsByTeacherIdQuery( Guid TeacherId) : IRequest<IReadOnlyList<TeacherInstrumentDto>>;