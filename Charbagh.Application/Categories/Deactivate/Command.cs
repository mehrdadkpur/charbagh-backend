
using Charbagh.Application.Categories.Common;
using MediatR;

namespace Charbagh.Application.Categories.Deactivate
{
    public sealed record DeactivateCategoryCommand(Guid Id) : IRequest<CategoryDto>;
}
