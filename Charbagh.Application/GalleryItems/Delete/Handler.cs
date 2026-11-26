using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.GalleryItems.Delete;

public sealed class DeleteGalleryItemHandler : IRequestHandler<DeleteGalleryItemCommand>
{
    private readonly IGalleryItemRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteGalleryItemHandler(
        IGalleryItemRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        DeleteGalleryItemCommand request,
        CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (item is null)
            throw new GalleryItemNotFoundException(request.Id);

        _repository.Remove(item);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
