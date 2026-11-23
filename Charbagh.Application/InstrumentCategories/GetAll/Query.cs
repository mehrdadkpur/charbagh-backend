using Charbagh.Application.InstrumentCategories.Common;
using MediatR;

namespace Charbagh.Application.InstrumentCategories.GetAll
{
    public sealed record GetAllInstrumentCategoriesQuery : IRequest<IReadOnlyList<InstrumentCategoryDto>>;
   
}
