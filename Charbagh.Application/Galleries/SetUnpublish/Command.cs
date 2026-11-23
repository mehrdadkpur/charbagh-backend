
using Charbagh.Application.Galleries.Common;
using MediatR;

namespace Charbagh.Application.Galleries.SetUnpublish
{
    public sealed record SetUnpublishGalleryCommand(Guid Id) : IRequest<GalleryDto>;
}
