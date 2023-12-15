using Money_Tracker.DAL.Entities;
using System.Data;


namespace Money_Tracker.DAL.Mappers
{
    public class CategoryMapper
    {
        public static Category Mapper(IDataRecord record)
        {
            return new Category
            {
                Id = (int)record["Category_Id"],
                Category_Name = (string)record["Category_Name"]
            };
        }
    }
}
