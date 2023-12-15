using Models = Money_Tracker.BLL.Models;
using Entities = Money_Tracker.DAL.Entities;

namespace Money_Tracker.BLL.Mappers
{
    public static class CategoryMapper
    {
        public static Models.Category ToModel(this Entities.Category entity)
        {
            return new Models.Category
            {
                Id = entity.Id,
                Category_Name = entity.Category_Name
            };
        }

        public static Entities.Category ToEntity(this Models.Category model)
        {
            return new Entities.Category
            {
                Id = model.Id,
                Category_Name = model.Category_Name
            };
        }
    }
}
