using Charbagh.Domain.Entities.Identity;

namespace Charbagh.Application.Permissions.Common
{
    public static class PermissionMapper
    {
        public static PermissionDto ToDto(this Permission permission)
        {
            return new PermissionDto(
                permission.Id , 
                permission.Module, 
                permission.Name , 
                permission.Action , 
                permission.DisplayName , 
                permission.Description , 
                permission.IsSystem
                );
        }
    }
}
