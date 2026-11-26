using Charbagh.Domain.Entities.CMS;

namespace Charbagh.Application.Contacts.Common;

public static class ContactMapper
{
    public static ContactDto ToDto(this Contact contact)
    {
        return new ContactDto(
            contact.Id,
            contact.FullName,
            contact.Mobile,
            contact.Status,
            contact.CreatedAt,
            contact.UpdatedAt);
    }
}
