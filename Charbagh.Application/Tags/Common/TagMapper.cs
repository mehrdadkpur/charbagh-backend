
using Charbagh.Domain.Entities.CMS;

namespace Charbagh.Application.Tags.Common
{
    public static class TagMapper
    {
        public static TagDto ToDto(this Tag tag)
        {
            return new TagDto(tag.Id, tag.Name, tag.IsActive);
        }
    }
}
