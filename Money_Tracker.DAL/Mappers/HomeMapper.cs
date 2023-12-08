using Money_Tracker.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.DAL.Mappers
{
    public class HomeMapper
    {
        public static Home Mapper(IDataRecord record)
        {
            return new Home
            {
                Id = (int)record["Home_Id"],
                User_Id = (int)record["User_Id"],
                Name_Home = (string)record["Name_Home"]
            };
        }
    }
}
