
namespace Charbagh.Application.Categories.Common
{
    public sealed record CategoryDto(
        Guid Id , 
        string Name,
        string? Description,
        bool IsActive
        );
}
