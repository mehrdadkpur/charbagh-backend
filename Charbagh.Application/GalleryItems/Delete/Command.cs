using MediatR;

namespace Charbagh.Application.GalleryItems.Delete;

public sealed record DeleteGalleryItemCommand(Guid Id) : IRequest;
