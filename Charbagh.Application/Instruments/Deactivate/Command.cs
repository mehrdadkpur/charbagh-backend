using Charbagh.Application.Instruments.Common;
using MediatR;

namespace Charbagh.Application.Instruments.Deactivate;

public sealed record DeactivateInstrumentCommand(Guid Id) : IRequest<InstrumentDto>;