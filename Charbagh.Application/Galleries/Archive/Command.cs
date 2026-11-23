
using Charbagh.Application.Galleries.Common;
using MediatR;

namespace Charbagh.Application.Galleries.Archive
{
    public sealed record ArchiveGalleryCommand(Guid Id) : IRequest<GalleryDto>;
}
