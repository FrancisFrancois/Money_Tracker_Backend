using Money_Tracker.BLL.Models;
using Money_Tracker.Tools.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.BLL.Interfaces
{
    public interface IExpenseService : ICrudService<int, Expense>
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
        //double GetTotalExpensesByCategoryByDay(DateTime date, int categoryId);
        //double GetTotalExpensesByCategoryByWeek(DateTime date, int categoryId);
        //double GetTotalExpensesByCategoryByMonth(DateTime date, int categoryId);
        //double GetTotalExpensesByCategoryByYear(DateTime date, int categoryId);

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
