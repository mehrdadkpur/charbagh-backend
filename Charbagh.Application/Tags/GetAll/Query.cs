
using Charbagh.Application.Tags.Common;
using MediatR;

namespace Charbagh.Application.Tags.GetAll
{
    public sealed record GetAllTagsQuery(): IRequest<IReadOnlyList< TagDto>>;
}
