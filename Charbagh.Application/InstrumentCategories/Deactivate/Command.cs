
using Charbagh.Application.InstrumentCategories.Common;
using MediatR;

namespace Charbagh.Application.InstrumentCategories.Deactivate
{
    public sealed record DeactivateInstrumentCategoryCommand(Guid Id) : IRequest<InstrumentCategoryDto>;
}
