using Charbagh.Application.Galleries.Common;
using MediatR;

namespace Charbagh.Application.Galleries.GetByTitle
{
    public sealed record GetByTitleGalleriesQuery(string Title): IRequest<IReadOnlyList< GalleryDto>>;
}
