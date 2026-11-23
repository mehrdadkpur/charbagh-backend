
using Charbagh.Application.Categories.Common;
using MediatR;

namespace Charbagh.Application.Categories.Update
{
    public sealed record UpdateCategoryCommand(Guid Id , string Name , string Description) : IRequest<CategoryDto>;
}
