using Charbagh.Application.Tags.Common;
using MediatR;

namespace Charbagh.Application.Tags.GetById
{
    public sealed record GetTagByIdQuery(Guid Id): IRequest<TagDto>;
}
