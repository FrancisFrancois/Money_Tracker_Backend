using Money_Tracker.DAL.Entities;
using Money_Tracker.Tools.Interfaces;

namespace Money_Tracker.DAL.Interfaces
{
    public interface IExpenseRepository : ICrud<int, Expense>
    {
        //IEnumerable<Expense> GetExpensesByDay(DateTime date);
        //IEnumerable<Expense> GetExpensesByWeek(DateTime date);
        //IEnumerable<Expense> GetExpensesByMonth(DateTime date);
        //IEnumerable<Expense> GetExpensesByYear(DateTime date);
        //double GetTotalExpensesByDay(DateTime date);
        //double GetTotalExpensesByWeek(DateTime date);
        //double GetTotalExpensesByMonth(DateTime date);
        //double GetTotalExpensesByYear(DateTime date);
        //IEnumerable<Expense> GetExpensesByCategoryByDay(DateTime date, int categoryId);
        //IEnumerable<Expense> GetExpensesByCategoryByWeek(DateTime date, int categoryId);
        //IEnumerable<Expense> GetExpensesByCategoryByMonth(DateTime date, int categoryId);
        //IEnumerable<Expense> GetExpensesByCategoryByYear(DateTime date, int categoryId);

        IEnumerable<Expense> GetExpensesByDay(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null);
        IEnumerable<Expense> GetExpensesByWeek(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null);
        IEnumerable<Expense> GetExpensesByMonth(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null);
        IEnumerable<Expense> GetExpensesByYear(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null);
        double GetTotalExpensesByDay(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null);
        double GetTotalExpensesByWeek(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null);
        double GetTotalExpensesByMonth(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null);
        double GetTotalExpensesByYear(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null);

    }
}

