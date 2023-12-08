using Money_Tracker.DAL.Entities;
using System.Data;


namespace Money_Tracker.DAL.Mappers
{
    public class HomeUserMapper
    {
        public static HomeUser MapperHS(IDataRecord record)
        {
            return new HomeUser
            {
                User_Id = (int)record["User_Id"],
                Home_Id = (int)record["Home_Id"]
            };
        }
    }
}

