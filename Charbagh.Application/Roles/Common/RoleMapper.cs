
using Charbagh.Domain.Entities.Identity;

namespace Charbagh.Application.Roles.Common
{
    public static class RoleMapper
    {
        public static RoleDto ToDto(this Role role)
        {
            return new RoleDto(
                role.Id, 
                role.Name, 
                role.DisplayName, 
                role.Description, 
                role.IsSystem, 
                role.IsActive
                );
        }
    }
}
