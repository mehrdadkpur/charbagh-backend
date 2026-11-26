
using Charbagh.Application.Categories.Common;
using MediatR;

namespace Charbagh.Application.Categories.Create
{
    public sealed record CreateCategoryCommand(
        string Name,
        string? Description
        ) :IRequest<CategoryDto>;
}
