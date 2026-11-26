using Charbagh.Application.Galleries.Common;
using MediatR;

namespace Charbagh.Application.Galleries.Create
{
    public sealed record CreateGalleryCommand(
        string Title,
        string? Description,
        string? CoverImageUrl) : IRequest<GalleryDto>;
}
