namespace Charbagh.Application.Blogs.Common
{
    public sealed record BlogDto(
        Guid Id,
        Guid UserId,
        Guid CategoryId,
        string FirstName,
        string LastName,
        string Title,
        string Text,
        string? ImageUrl
        );
}
