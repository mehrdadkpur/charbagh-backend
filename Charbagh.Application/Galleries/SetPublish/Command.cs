
using Charbagh.Application.Galleries.Common;
using MediatR;

namespace Charbagh.Application.Galleries.SetPublish
{
    public sealed record SetPublishGalleryCommand(Guid Id) : IRequest<GalleryDto>;
}
