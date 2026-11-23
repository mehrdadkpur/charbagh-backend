using Charbagh.Application.TeacherInstruments.Common;
using MediatR;

namespace Charbagh.Application.TeacherInstruments.GetById;

public sealed record GetTeacherInstrumentByIdQuery(Guid Id) : IRequest<TeacherInstrumentDto>;