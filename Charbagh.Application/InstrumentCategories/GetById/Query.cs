using Charbagh.Application.InstrumentCategories.Common;
using MediatR;

namespace Charbagh.Application.InstrumentCategories.GetById
{
    public sealed record GetInstrumentCategoryByIdQuery(Guid Id) : IRequest<InstrumentCategoryDto>;
   
}
