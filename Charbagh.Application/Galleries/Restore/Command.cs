
using Charbagh.Application.Galleries.Common;
using MediatR;

namespace Charbagh.Application.Galleries.Restore
{
    public sealed record RestoreGalleryCommand(Guid Id) : IRequest<GalleryDto>;
}
