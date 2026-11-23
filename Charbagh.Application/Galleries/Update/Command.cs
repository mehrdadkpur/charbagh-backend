
using Charbagh.Application.Galleries.Common;
using MediatR;

namespace Charbagh.Application.Galleries.Update
{
    public sealed record UpdateGalleryCommand(
        Guid Id , 
        string Title , 
        string Description , 
        string CoverImageUrl
        ) : IRequest<GalleryDto>;
}
