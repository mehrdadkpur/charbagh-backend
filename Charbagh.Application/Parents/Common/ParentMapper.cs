
using Charbagh.Domain.Entities.People;

namespace Charbagh.Application.Parents.Common
{
    public static class ParentMapper
    {
        public static ParentDto ToDto ( this Parent parent)
        {
            ArgumentNullException.ThrowIfNull(parent);

            var user = parent.User
                ?? throw new InvalidOperationException(
                    $"User navigation property is null for PArent: {parent.Id}");
            return new ParentDto(
                parent.Id ,
                parent.UserId,
                user.FirstName ,
                user.LastName ,
                user.Email ,
                user.NationalIdentificationNumber,
                user.ProfileImageUrl,
                parent.Status
                );
        }
    }
}
