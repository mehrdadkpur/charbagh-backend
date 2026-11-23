using Charbagh.Application.Instruments.Common;
using MediatR;

namespace Charbagh.Application.Instruments.GetById;

public sealed record GetInstrumentByIdQuery(Guid Id) : IRequest<InstrumentDto>;