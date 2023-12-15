using Models = Money_Tracker.BLL.Models;
using Entities = Money_Tracker.DAL.Entities;

namespace Money_Tracker.BLL.Mappers
{
    public static class ExpenseMapper
    {
        public static Models.Expense ToModel(this Entities.Expense entity)
        {
            return new Models.Expense
            {
                Id = entity.Id,
                Category_Id = entity.Category_Id,
                User_Id = entity.User_Id,
                Home_Id = entity.Home_Id,
                Amount = entity.Amount,
                Description = entity.Description,
                Date_Expense = entity.Date_Expense,
            };
        }

        public static Entities.Expense ToEntity(this Models.Expense model)
        {
            return new Entities.Expense
            {
                Id = model.Id,
                Category_Id = model.Category_Id,
                User_Id = model.User_Id,
                Home_Id = model.Home_Id,
                Amount = model.Amount,
                Description = model.Description,
                Date_Expense = model.Date_Expense,
            };
        }
    }
}
