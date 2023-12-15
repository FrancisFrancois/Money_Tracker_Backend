using Money_Tracker.API.DTOs;
using Money_Tracker.BLL.Models;

namespace Money_Tracker.API.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDTO ToDTO(this Category model)
        {
            return new CategoryDTO
            {
                Id = model.Id,
                Category_Name = model.Category_Name
            };
        }

        public static Category ToModel(this CategoryDataDTO category)
        {
            return new Category
            {
                Category_Name = category.Category_Name
            };
        }
    }
}
