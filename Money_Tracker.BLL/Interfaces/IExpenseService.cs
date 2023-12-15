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
        IEnumerable<Expense> GetExpensesByDay(DateTime date);
        IEnumerable<Expense> GetExpensesByWeek(DateTime date);
        IEnumerable<Expense> GetExpensesByMonth(DateTime date);
        IEnumerable<Expense> GetExpensesByYear(DateTime date);
    }
}
