using Money_Tracker.BLL.Interfaces;
using Money_Tracker.BLL.Mappers;
using Money_Tracker.BLL.Models;
using Money_Tracker.DAL.Interfaces;
using Money_Tracker.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.BLL.Services
{
    public class ExpenseService : IExpenseService   
    {
        private readonly IExpenseRepository _ExpenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _ExpenseRepository = expenseRepository;
        }
        public IEnumerable<Expense> GetAll()
        {
           return _ExpenseRepository.GetAll().Select(e => e.ToModel());
        }

        public Expense? GetById(int id)
        {
            return _ExpenseRepository.GetById(id)?.ToModel();
        }

        public Expense Create(Expense expense)
        {
            return _ExpenseRepository.Create(expense.ToEntity()).ToModel();
        }

        public bool Update(int id, Expense expense)
        {
            bool updated = _ExpenseRepository.Update(id, expense.ToEntity());
            if (!updated)
            {
                throw new DirectoryNotFoundException("Expense not found");
            }
            return updated;
        }

        public bool Delete(int id)
        {
            bool deleted = _ExpenseRepository.Delete(id);
            if (!deleted)
            {
                throw new DirectoryNotFoundException("Expense not found");
            }
            return deleted;
        }

        #region Unused

        //    public IEnumerable<Expense> GetExpensesByDay(DateTime date)
        //    {
        //        return _ExpenseRepository.GetExpensesByDay(date).Select(e => e.ToModel());
        //    }

        //    public IEnumerable<Expense> GetExpensesByWeek(DateTime date)
        //    {
        //        return _ExpenseRepository.GetExpensesByWeek(date).Select(e => e.ToModel());
        //    }

        //    public IEnumerable<Expense> GetExpensesByMonth(DateTime date)
        //    {
        //        return _ExpenseRepository.GetExpensesByMonth(date).Select(e => e.ToModel());
        //    }

        //    public IEnumerable<Expense> GetExpensesByYear(DateTime date)
        //    {
        //        return _ExpenseRepository.GetExpensesByYear(date).Select(e => e.ToModel());
        //    }
        //    public double GetTotalExpensesByDay(DateTime date)
        //    {
        //        return GetExpensesByDay(date).Sum(expense => expense.Amount);
        //    }

        //    public double GetTotalExpensesByWeek(DateTime date)
        //    {
        //        return GetExpensesByWeek(date).Sum(expense => expense.Amount);
        //    }

        //    public double GetTotalExpensesByMonth(DateTime date)
        //    {
        //        return GetExpensesByMonth(date).Sum(expense => expense.Amount);
        //    }

        //    public double GetTotalExpensesByYear(DateTime date)
        //    {
        //        return GetExpensesByYear(date).Sum(expense => expense.Amount);
        //    }

        //    public IEnumerable<Expense> GetExpensesByCategoryByDay(DateTime date, int categoryId)
        //    {
        //        return _ExpenseRepository.GetExpensesByCategoryByDay(date, categoryId).Select(e => e.ToModel());
        //    }

        //    public IEnumerable<Expense> GetExpensesByCategoryByWeek(DateTime date, int categoryId)
        //    {
        //        return _ExpenseRepository.GetExpensesByCategoryByWeek(date, categoryId).Select(e => e.ToModel());
        //    }

        //    public IEnumerable<Expense> GetExpensesByCategoryByMonth(DateTime date, int categoryId)
        //    {
        //        return _ExpenseRepository.GetExpensesByCategoryByMonth(date, categoryId).Select(e => e.ToModel());
        //    }

        //    public IEnumerable<Expense> GetExpensesByCategoryByYear(DateTime date, int categoryId)
        //    {
        //        return _ExpenseRepository.GetExpensesByCategoryByYear(date, categoryId).Select(e => e.ToModel());
        //    }

        //    public double GetTotalExpensesByCategoryByDay(DateTime date, int categoryId)
        //    {
        //        return GetExpensesByCategoryByDay(date, categoryId).Sum(expense => expense.Amount);
        //    }

        //    public double GetTotalExpensesByCategoryByWeek(DateTime date, int categoryId)
        //    {
        //        return GetExpensesByCategoryByWeek(date, categoryId).Sum(expense => expense.Amount);
        //    }

        //    public double GetTotalExpensesByCategoryByMonth(DateTime date, int categoryId)
        //    {
        //        return GetExpensesByCategoryByMonth(date, categoryId).Sum(expense => expense.Amount);
        //    }

        //    public double GetTotalExpensesByCategoryByYear(DateTime date, int categoryId)
        //    {
        //        return GetExpensesByCategoryByYear(date, categoryId).Sum(expense => expense.Amount);
        //    }

        #endregion

        public IEnumerable<Expense> GetExpensesByDay(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null)
        {
            try
            {
                DateTime startDate = date.Date;
                DateTime endDate = startDate.AddDays(1).AddSeconds(-1);
                var expenses = _ExpenseRepository.GetExpenses(startDate, endDate, homeId, userId, categoryId);
                return expenses.Select(e => e.ToModel());
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get expenses by day", ex);
            }
        }

        public IEnumerable<Expense> GetExpensesByWeek(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null)
        {   
            try
            {
            DateTime startDate = date.AddDays(-(int)date.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime endDate = startDate.AddDays(7).AddSeconds(-1);
            return _ExpenseRepository.GetExpenses(startDate, endDate, homeId, userId, categoryId).Select(e => e.ToModel());
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get expenses by week", ex);
            }
        }

        public IEnumerable<Expense> GetExpensesByMonth(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null)
        {   
            try
            {
            DateTime startDate = new DateTime(date.Year, date.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            return _ExpenseRepository.GetExpenses(startDate, endDate, homeId, userId, categoryId).Select(e => e.ToModel());
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get expenses by month", ex);
            }
        }

        public IEnumerable<Expense> GetExpensesByYear(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null)
        {   
            try
            {
            DateTime startDate = new DateTime(date.Year, 1, 1);
            DateTime endDate = new DateTime(date.Year, 12, 31);
            return _ExpenseRepository.GetExpenses(startDate, endDate, homeId, userId, categoryId).Select(e => e.ToModel());
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get expenses by year", ex);
            }
        }

        public double GetTotalExpensesByDay(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null)
        {
            try
            {
            DateTime startDate = date.Date;
            DateTime endDate = startDate.AddDays(1).AddSeconds(-1);
            return _ExpenseRepository.CalculateTotalExpenses(startDate, endDate, homeId, userId, categoryId);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get total expenses by day", ex);
            }
        }

        public double GetTotalExpensesByWeek(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null)
        {   
            try
            {
            DateTime startDate = date.AddDays(-(int)date.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime endDate = startDate.AddDays(7).AddSeconds(-1);
            return _ExpenseRepository.CalculateTotalExpenses(startDate, endDate, homeId, userId, categoryId);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get total expenses by week", ex);
            }
        }

        public double GetTotalExpensesByMonth(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null)
        {   
            try
            {
            DateTime startDate = new DateTime(date.Year, date.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            return _ExpenseRepository.CalculateTotalExpenses(startDate, endDate, homeId, userId, categoryId);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get total expenses by month", ex);
            }
        }

        public double GetTotalExpensesByYear(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null)
        {   
            try
            {
            DateTime startDate = new DateTime(date.Year, 1, 1);
            DateTime endDate = new DateTime(date.Year, 12, 31);
            return _ExpenseRepository.CalculateTotalExpenses(startDate, endDate, homeId, userId, categoryId);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get total expenses by year", ex);
            }
        }
    }
}
