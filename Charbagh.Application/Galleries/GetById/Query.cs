using Charbagh.Application.Galleries.Common;
using MediatR;

namespace Charbagh.Application.Galleries.GetById
{
    public sealed record GetGalleryByIdQuery(Guid Id) : IRequest<GalleryDto>;
}
