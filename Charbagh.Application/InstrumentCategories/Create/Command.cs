
using Charbagh.Application.InstrumentCategories.Common;
using MediatR;

namespace Charbagh.Application.InstrumentCategories.Create;

public sealed record CreateInstrumentCategoryCommand( string Name, string? Description): IRequest<InstrumentCategoryDto>;