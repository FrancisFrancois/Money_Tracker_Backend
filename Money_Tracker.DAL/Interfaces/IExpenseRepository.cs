using Money_Tracker.DAL.Entities;
using Money_Tracker.Tools.Interfaces;

namespace Money_Tracker.DAL.Interfaces
{
    public interface IExpenseRepository : ICrud<int, Expense>
    {
        IEnumerable<Expense> GetExpensesByDay(DateTime date);
        IEnumerable<Expense> GetExpensesByWeek(DateTime date);
        IEnumerable<Expense> GetExpensesByMonth(DateTime date);
        IEnumerable<Expense> GetExpensesByYear(DateTime date);

    }
}

