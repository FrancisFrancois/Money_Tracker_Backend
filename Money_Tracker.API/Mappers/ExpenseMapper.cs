using Money_Tracker.API.DTOs;
using Money_Tracker.BLL.Models;

namespace Money_Tracker.API.Mappers
{
    public static class ExpenseMapper
    {
        public static ExpenseDTO ToDTO(this Expense model)
        {
            return new ExpenseDTO
            {
                Id = model.Id,
                Category_Id = model.Category_Id,
                User_Id = model.User_Id,
                Home_Id = model.Home_Id,
                Amount = model.Amount,
                Description = model.Description,
                Date_Expense = model.Date_Expense
            };
        }

        public static Expense ToModel(this ExpenseDataDTO expense)
        {
            return new Expense
            {
                Category_Id = expense.Category_Id,
                User_Id = expense.User_Id,
                Home_Id = expense.Home_Id,
                Amount = expense.Amount,
                Description = expense.Description,
                Date_Expense = expense.Date_Expense
            };
        }
    }
}
