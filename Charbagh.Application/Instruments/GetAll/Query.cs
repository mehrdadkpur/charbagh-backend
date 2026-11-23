
using Charbagh.Application.Instruments.Common;
using MediatR;

namespace Charbagh.Application.Instruments.GetAll;

public sealed record GetAllUserssQuery : IRequest<IReadOnlyList<InstrumentDto>>;