namespace Charbagh.Application.Common.Exceptions;

public sealed class GalleryItemNotFoundException : Exception
{
    public GalleryItemNotFoundException(Guid Id)
        : base($"This Gallery Item {Id} Not Found")
    {
    }
}