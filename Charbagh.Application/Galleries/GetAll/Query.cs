using Charbagh.Application.Galleries.Common;
using MediatR;

namespace Charbagh.Application.Galleries.GetAll
{
    public sealed record GetAllGalleriesQuery(): IRequest<IReadOnlyList< GalleryDto>>;
}
