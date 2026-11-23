using Charbagh.Application.Categories.Common;
using MediatR;

namespace Charbagh.Application.Categories.GetById
{
    public sealed record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDto>;
}
