using Money_Tracker.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.DAL.Mappers
{
    public class ExpenseMapper
    {
        public static Expense Mapper(IDataRecord record)
        {
            return new Expense
            {
                Id = (int)record["Id"],
                User_Id = (int)record["User_Id"],
                Home_Id = (int)record["Home_Id"],
                Amount = (float)record["Amount"],
                Description = (string)record["Description"],
                Date_Expense = (DateTime)record["Date_Expense"]
            };
        }
    }
}
