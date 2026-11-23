
using Charbagh.Application.Categories.Common;
using MediatR;

namespace Charbagh.Application.Categories.Activate
{
    public sealed record ActivateCategoryCommand(Guid Id) : IRequest<CategoryDto>;
}
