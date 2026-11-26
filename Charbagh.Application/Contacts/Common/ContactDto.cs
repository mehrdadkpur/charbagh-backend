namespace Charbagh.Application.Contacts.Common;

public sealed record ContactDto(
    Guid Id,
    string FullName,
    string Mobile,
    ContactStatus Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
