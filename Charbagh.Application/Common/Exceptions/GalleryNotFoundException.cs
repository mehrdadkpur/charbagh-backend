namespace Charbagh.Application.Common.Exceptions;

public sealed class GalleryNotFoundException : Exception
{
    public GalleryNotFoundException(Guid Id)
        : base($"This Gallery {Id} Not Found")
    {
    }
}