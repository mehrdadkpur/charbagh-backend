using Charbagh.Application.InstrumentCategories.Common;
using MediatR;

namespace Charbagh.Application.InstrumentCategories.Activate
{
    public sealed record ActivateInstrumentCategoryCommand(Guid Id) : IRequest<InstrumentCategoryDto>;
  
}
