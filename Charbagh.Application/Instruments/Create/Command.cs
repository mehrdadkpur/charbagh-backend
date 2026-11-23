
using Charbagh.Application.Instruments.Common;
using MediatR;

namespace Charbagh.Application.Instruments.Create;

public sealed record CreatebookCommand( Guid InstrumentCategoryId, string Name, string? EnglishName, string? Description): IRequest<InstrumentDto>;