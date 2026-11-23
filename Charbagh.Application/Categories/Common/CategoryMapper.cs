namespace Charbagh.Application.Categories.Common
{
    public static class CategoryMapper
    {
        public static CategoryDto ToDto(this Category category)
        {
            return new CategoryDto(
                category.Id,
                category.Name,
                category?.Description,
                category.IsActive
                );
        }
    }
}
