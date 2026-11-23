using Charbagh.Application.TeacherInstruments.Common;
using MediatR;

namespace Charbagh.Application.TeacherInstruments.Deactivate;

public sealed record DeactivateTeacherInstrumentCommand( Guid Id) : IRequest<TeacherInstrumentDto>;