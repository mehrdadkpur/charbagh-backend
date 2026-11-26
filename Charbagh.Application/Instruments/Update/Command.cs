using Charbagh.Application.Instruments.Common;
using MediatR;

namespace Charbagh.Application.Instruments.Update;

public sealed record UpdateInstrumentCommand( Guid Id, Guid InstrumentCategoryId, string Name , string? EnglishName , string? Description) : IRequest<InstrumentDto>;