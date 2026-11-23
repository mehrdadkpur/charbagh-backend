
using Charbagh.Application.Categories.Common;
using MediatR;

namespace Charbagh.Application.Categories.GetAll
{
    public sealed record GetAllCategoriesQuery() : IRequest<IReadOnlyList<CategoryDto>>;
}
