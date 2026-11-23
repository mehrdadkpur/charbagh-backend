using Charbagh.Application.TeacherInstruments.Common;
using MediatR;

namespace Charbagh.Application.TeacherInstruments.GetAll;

public sealed record GetAllTeacherInstrumentsQuery: IRequest<IReadOnlyList<TeacherInstrumentDto>>;