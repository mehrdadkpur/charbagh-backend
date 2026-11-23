using Charbagh.Application.TeacherInstruments.Common;
using MediatR;

namespace Charbagh.Application.TeacherInstruments.Activate;

public sealed record ActivateTeacherInstrumentCommand(Guid Id) : IRequest<TeacherInstrumentDto>;