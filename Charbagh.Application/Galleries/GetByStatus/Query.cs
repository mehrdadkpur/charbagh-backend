using Charbagh.Application.Galleries.Common;
using MediatR;

namespace Charbagh.Application.Galleries.GetByStatus
{
    public sealed record GetByStatusGalleriesQuery(GalleryStatus Status): IRequest<IReadOnlyList< GalleryDto>>;
}
