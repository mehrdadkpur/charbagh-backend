using Charbagh.Application.Instruments.Common;
using MediatR;

namespace Charbagh.Application.Instruments.Activate;

public sealed record ActivateInstrumentCommand(Guid Id) : IRequest<InstrumentDto>;