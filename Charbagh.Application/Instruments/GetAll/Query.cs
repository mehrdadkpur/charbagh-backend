
using Charbagh.Application.Instruments.Common;
using MediatR;

namespace Charbagh.Application.Instruments.GetAll;

public sealed record GetAllInstrumentsQuery : IRequest<IReadOnlyList<InstrumentDto>>;